declare @���� varchar(200),@������ varchar(500),@������� int,@sql varchar(2000)
declare @tableResult table(sqlcontent varchar(2000));
declare ReBuildIndex_Cursor Cursor scroll For
SELECT  c.name ����,a.name ������, a.OrigFillFactor �������
FROM   sysindexes   a  
JOIN   sysindexkeys   b   ON   a.id=b.id   AND   a.indid=b.indid  
JOIN   sysobjects   c   ON   b.id=c.id  
JOIN   syscolumns   d   ON   b.id=d.id   AND   b.colid=d.colid  
WHERE   a.indid   NOT IN(0,255)  
 and   c.xtype='U'   --�������û���  
ORDER BY   c.name,a.name,d.name 
open ReBuildIndex_Cursor
fetch ReBuildIndex_Cursor into @����,@������,@�������
while(@@fetch_status = 0)
begin
DBCC DBREINDEX(@����,@������,@�������)
select @sql='DBCC DBREINDEX('+@����+','+@������+','+cast(@������� as varchar(200))+')'
insert into @tableResult (sqlcontent) values(@sql)
fetch next from ReBuildIndex_Cursor into @����,@������,@�������
end
CLOSE ReBuildIndex_Cursor
deallocate ReBuildIndex_Cursor
select * from @tableResult
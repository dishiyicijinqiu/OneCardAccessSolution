declare @表名 varchar(200),@索引名 varchar(500),@填充银子 int,@sql varchar(2000)
declare @tableResult table(sqlcontent varchar(2000));
declare ReBuildIndex_Cursor Cursor scroll For
SELECT  c.name 表名,a.name 索引名, a.OrigFillFactor 填充银子
FROM   sysindexes   a  
JOIN   sysindexkeys   b   ON   a.id=b.id   AND   a.indid=b.indid  
JOIN   sysobjects   c   ON   b.id=c.id  
JOIN   syscolumns   d   ON   b.id=d.id   AND   b.colid=d.colid  
WHERE   a.indid   NOT IN(0,255)  
 and   c.xtype='U'   --查所有用户表  
ORDER BY   c.name,a.name,d.name 
open ReBuildIndex_Cursor
fetch ReBuildIndex_Cursor into @表名,@索引名,@填充银子
while(@@fetch_status = 0)
begin
DBCC DBREINDEX(@表名,@索引名,@填充银子)
select @sql='DBCC DBREINDEX('+@表名+','+@索引名+','+cast(@填充银子 as varchar(200))+')'
insert into @tableResult (sqlcontent) values(@sql)
fetch next from ReBuildIndex_Cursor into @表名,@索引名,@填充银子
end
CLOSE ReBuildIndex_Cursor
deallocate ReBuildIndex_Cursor
select * from @tableResult
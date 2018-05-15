-------------------------------------------------------------------------------
use [SMS]
go
-------------------------------------------------------------------------------
--网站信息
-------------------------------------------------------------------------------
if object_id('[SMS_SiteInfo]','u') is not null	drop table [SMS_SiteInfo]
go
create table [SMS_SiteInfo](
	[SiteName] [nvarchar](50) not null,
	[SiteDomain] [nvarchar](50) not null,
	[SiteEmail] [nvarchar](50) not null,
	[SiteKeywords] [nvarchar](50),
	[SiteDescription] [nvarchar](50),
	[SiteCopyright] [ntext]
)
insert into [SMS_SiteInfo] values('TsoLong 网站管理系统','http://www.tsolong.com/','tsolong@126.com','网站开发，网站设计，网站优化。','TsoLong 网站管理系统','Copyright &copy; 2009 <a href="http://www.tsolong.com/" target="_blank">TsoLong 网站管理系统 V1.0</a> 版权所有')
--select * from [SMS_SiteInfo]
go
-------------------------------------------------------------------------------
--编辑器页面
-------------------------------------------------------------------------------
if object_id('[SMS_Editor]','u') is not null	drop table [SMS_Editor]
go
create table [SMS_Editor](
	[ID] [int] not null primary key,
	[Name] [nvarchar](10) not null,
	[Content] [ntext] not null
)
insert into [SMS_Editor]([ID],[Name],[Content]) values(1,'关于我们','关于我们')
insert into [SMS_Editor]([ID],[Name],[Content]) values(2,'联系我们','联系我们')
--select * from [SMS_Editor]
go
-------------------------------------------------------------------------------
--新闻分类
-------------------------------------------------------------------------------
if object_id('[SMS_News_Category]','u') is not null	drop table [SMS_News_Category]
go
create table [SMS_News_Category](
	[ID] [int] identity(1,1) not null primary key,
	[CategoryName] [nvarchar](10) not null,
	[OrderID] [int] not null,
	[CreateDate] [datetime] not null default (getdate())
)
insert into [SMS_News_Category]([CategoryName],[OrderID]) values('企业新闻','1')
insert into [SMS_News_Category]([CategoryName],[OrderID]) values('行业新闻','2')
--select * from [SMS_News_Category]
go
-------------------------------------------------------------------------------
--新闻
-------------------------------------------------------------------------------
if object_id('[SMS_News]','u') is not null	drop table [SMS_News]
go
create table [SMS_News](
	[ID] [int] identity(1,1) not null primary key,
	[CategoryID] [int] not null,
	[Title] [nvarchar](50) not null,
	[Content] [ntext] not null,
	[Hits] [int] not null default (0),
	[CreateDate] [datetime] not null default (getdate())
)
insert into [SMS_News]([CategoryID],[Title],[Content]) values(1,'企业新闻','新闻内容')
insert into [SMS_News]([CategoryID],[Title],[Content]) values(2,'行业新闻','新闻内容')
--select * from [SMS_News]
go
-------------------------------------------------------------------------------
--产品分类
-------------------------------------------------------------------------------
if object_id('[SMS_Products_Category]','u') is not null	drop table [SMS_Products_Category]
go
create table [SMS_Products_Category](
	[ID] [int] identity(1,1) not null primary key,
	[ParentID] [int] not null,
	[CategoryName] [nvarchar](10) not null,
	[OrderID] [int] not null,
	[CreateDate] [datetime] not null default (getdate())
)
insert into [SMS_Products_Category]([ParentID],[CategoryName],[OrderID]) values(0,'笔记本','1')
insert into [SMS_Products_Category]([ParentID],[CategoryName],[OrderID]) values(1,'惠普','1')
insert into [SMS_Products_Category]([ParentID],[CategoryName],[OrderID]) values(1,'华硕','2')
insert into [SMS_Products_Category]([ParentID],[CategoryName],[OrderID]) values(1,'联想','3')
--select * from [SMS_Products_Category]
go
-------------------------------------------------------------------------------
--产品
-------------------------------------------------------------------------------
if object_id('[SMS_Products]','u') is not null	drop table [SMS_Products]
go
create table [SMS_Products](
	[ID] [int] identity(1,1) not null primary key,
	[CategoryID] [int] not null,
	[Name] [nvarchar](50) not null,
	[Picture] [nvarchar](40) not null,
	[Content] [ntext] not null,
	[Hits] [int] not null default (0),
	[CreateDate] [datetime] not null default (getdate())
)
insert into [SMS_Products]([CategoryID],[Name],[Picture],[Content]) values(2,'HP Pavilion 畅游人 dv3-2100 娱乐笔记本电脑系列','200909141646025312795/o.jpg','HP Pavilion 畅游人 dv3-2100 娱乐笔记本电脑系列')
insert into [SMS_Products]([CategoryID],[Name],[Picture],[Content]) values(3,'华硕 视讯系列 X8AIN','200909141650077031848/o.jpg','华硕 视讯系列 X8AIN')
insert into [SMS_Products]([CategoryID],[Name],[Picture],[Content]) values(4,'联想 昭阳 K23','200909141653187656510/o.jpg','联想 昭阳 K23')
--select * from [SMS_Products]
go
-------------------------------------------------------------------------------
--招聘
-------------------------------------------------------------------------------
if object_id('[SMS_Jobs]','u') is not null	drop table [SMS_Jobs]
go
create table [SMS_Jobs](
	[ID] [int] identity(1,1) not null primary key,
	[Name] [nvarchar](10) not null,
	[Dept] [nvarchar](10) not null,
	[Email] [nvarchar](30) not null,
	[Address] [nvarchar](50) not null,
	[Num] [nvarchar](10) not null,
	[Sex] [nvarchar](10) not null,
	[Experience] [nvarchar](10) not null,
	[Language] [nvarchar](10) not null,
	[Salary] [nvarchar](10) not null,
	[Education] [nvarchar](10) not null,
	[ResumeLanguage] [nvarchar](10) not null,
	[Content] [ntext] not null,
	[Hits] [int] not null default (0),
	[State] [bit] not null default (1),
	[CreateDate] [datetime] not null default (getdate())
)
insert into [SMS_Jobs]([Name],[Dept],[Email],[Address],[Num],[Sex],[Experience],[Language],[Salary],[Education],[ResumeLanguage],[Content]) values('程序员','技术部','tsolong@126.com','上海','10','男','2年以上','英语','面议','本科','中文','招聘程序员')
--select * from [SMS_Jobs]
go
-------------------------------------------------------------------------------
--留言
-------------------------------------------------------------------------------
if object_id('[SMS_Leaveword]','u') is not null	drop table [SMS_Leaveword]
go
create table [SMS_Leaveword](
	[ID] [int] identity(1,1) not null primary key,
	[Name] [nvarchar](10) not null,
	[Email] [nvarchar](30) not null,
	[Phone] [nvarchar](20) not null,
	[Content] [ntext] not null,
	[View] [bit] not null default (0),
	[Revert] [bit] not null default (0),
	[RevertContent] [ntext],
	[CreateDate] [datetime] not null default (getdate())
)
insert into [SMS_Leaveword]([Name],[Email],[Phone],[Content]) values('TsoLong','tsolong@126.com','13818791403','网站管理员您好')
--select * from [SMS_Leaveword]
go
-------------------------------------------------------------------------------
--友情链接
-------------------------------------------------------------------------------
if object_id('[SMS_Links]','u') is not null	drop table [SMS_Links]
go
create table [SMS_Links](
	[ID] [int] identity(1,1) not null primary key,
	[SiteName] [nvarchar](50) not null,
	[SiteDomain] [nvarchar](50) not null,
	[LogoAddress] [nvarchar](50) not null,
	[LogoWidth] [nvarchar](10) not null,
	[LogoHeight] [nvarchar](10) not null,
	[CreateDate] [datetime] not null default (getdate())
)
--文字链接
insert into [SMS_Links]([SiteName],[SiteDomain],[LogoAddress],[LogoWidth],[LogoHeight]) values('TsoLong Blog','http://www.tsolong.com/','','','')
--图片链接
insert into [SMS_Links]([SiteName],[SiteDomain],[LogoAddress],[LogoWidth],[LogoHeight]) values('TsoLong Blog','http://www.tsolong.com/','http://www.tsolong.com/other/logo.gif','88','31')
--select * from [SMS_Links]
go
-------------------------------------------------------------------------------
--系统用户
-------------------------------------------------------------------------------
if object_id('[SMS_Sys_User]','u') is not null	drop table [SMS_Sys_User]
go
create table [SMS_Sys_User](
	[UserID] [int] identity(1,1) not null primary key,
	[UserName] [nvarchar](16) not null,
	[Password] [nvarchar](16) not null,
	[LastLoginDate] [datetime],
	[LastLoginIP] [nvarchar](15),
	[CreateDate] [datetime] not null default (getdate())
)
insert into [SMS_Sys_User]([UserName],[Password]) values('admin','7a57a5a743894a0e')
--select * from [SMS_Sys_User]
go
-------------------------------------------------------------------------------
--分页存储过程
-------------------------------------------------------------------------------
if(object_id('DataPage ','p') is not null) drop procedure [DataPage]
go

create procedure [DataPage]
	@TableName nvarchar(100),		--数据库表名
	@SelectColumnName nvarchar(1000) = '' ,	--查询的列名
	@SelectWhere nvarchar(1500) = '',	--查询的条件
	@OrderColumnName nvarchar(255) = '',	--排序的字段 (不能为空)
	@OrderType bit = 0,			--排序类型 (0||false:升序 非0||true:降序)
	@PageSize int = 10,			--每页数据条数
	@PageIndex int = 1,			--当前页码
	@RecordTotal int output 		--记录总数
as
set nocount on
declare @StrTotalSQL nvarchar(4000)
declare @StrSQL nvarchar(4000)
declare @StrTemp nvarchar(1000)
declare @StrOrder nvarchar(1000)
	
	--记录总数
	if @SelectWhere != ''
		set @StrTotalSQL = 'select @RecordTotal=count(*) from ' + @TableName + ' where ' + @SelectWhere
	else
		set @StrTotalSQL = 'select @RecordTotal=count(*) from ' + @TableName
	

	if @OrderType != 0
		begin
			set @StrTemp = '<(select min'
			set @StrOrder = 'order by ' + @OrderColumnName + ' desc'
		end
	else
		begin
			set @StrTemp = '>(select max'
			set @StrOrder = 'order by ' + @OrderColumnName + ' asc'
		end

	if @PageIndex = 1
		begin
			if @SelectWhere != ''
				set @StrSQL = 'select top ' + str(@PageSize) + ' ' + @SelectColumnName + ' from ' + @TableName + ' where ' + @SelectWhere + ' ' + @StrOrder
			else
				set @StrSQL = 'select top ' + str(@PageSize) + ' ' + @SelectColumnName + ' from ' + @TableName + ' ' + @StrOrder
	
		end
	else
		begin
			set @OrderColumnName = substring(@OrderColumnName,charindex('.',@OrderColumnName)+1,len(@OrderColumnName) - charindex('.',@OrderColumnName))
			
			if @SelectWhere = ''
				set @StrSQL = 'select top ' + str(@PageSize) + ' ' + @SelectColumnName + ' from ' 
				+ @TableName + ' where ' + @OrderColumnName + '' + @StrTemp 
				+ '(' + @OrderColumnName + ') from (select top ' + str((@PageIndex - 1) * @PageSize) 
				+ ' ' + @OrderColumnName + ' from ' + @TableName + ' ' + @StrOrder + ') as TempColumn) ' 
				+ @StrOrder
			else
				set @StrSQL = 'select top ' + str(@PageSize) + ' ' + @SelectColumnName + ' from ' 
				+ @TableName + ' where ' + @OrderColumnName + '' + @StrTemp 
				+ '(' + @OrderColumnName + ') from (select top ' + str((@PageIndex - 1) * @PageSize) 
				+ ' ' + @OrderColumnName + ' from ' + @TableName + ' where ' + @SelectWhere + ' ' + @StrOrder + ') as TempColumn) and ' 
				+ @SelectWhere + ' ' + @StrOrder
		end

execute sp_executesql @StrTotalSQL,N'@RecordTotal int output',@RecordTotal output
execute (@StrSQL)
go
/*print @StrTotalSQL
print @StrSQL*/
go
/*declare @RT int
EXECUTE DataPage 'SMS_News','*','',"CreateDate",true,10,1,@RT output
print @RT*/
go
-------------------------------------------------------------------------------
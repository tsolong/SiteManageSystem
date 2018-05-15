-------------------------------------------------------------------------------
use [SMS]
go
-------------------------------------------------------------------------------
--��վ��Ϣ
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
insert into [SMS_SiteInfo] values('TsoLong ��վ����ϵͳ','http://www.tsolong.com/','tsolong@126.com','��վ��������վ��ƣ���վ�Ż���','TsoLong ��վ����ϵͳ','Copyright &copy; 2009 <a href="http://www.tsolong.com/" target="_blank">TsoLong ��վ����ϵͳ V1.0</a> ��Ȩ����')
--select * from [SMS_SiteInfo]
go
-------------------------------------------------------------------------------
--�༭��ҳ��
-------------------------------------------------------------------------------
if object_id('[SMS_Editor]','u') is not null	drop table [SMS_Editor]
go
create table [SMS_Editor](
	[ID] [int] not null primary key,
	[Name] [nvarchar](10) not null,
	[Content] [ntext] not null
)
insert into [SMS_Editor]([ID],[Name],[Content]) values(1,'��������','��������')
insert into [SMS_Editor]([ID],[Name],[Content]) values(2,'��ϵ����','��ϵ����')
--select * from [SMS_Editor]
go
-------------------------------------------------------------------------------
--���ŷ���
-------------------------------------------------------------------------------
if object_id('[SMS_News_Category]','u') is not null	drop table [SMS_News_Category]
go
create table [SMS_News_Category](
	[ID] [int] identity(1,1) not null primary key,
	[CategoryName] [nvarchar](10) not null,
	[OrderID] [int] not null,
	[CreateDate] [datetime] not null default (getdate())
)
insert into [SMS_News_Category]([CategoryName],[OrderID]) values('��ҵ����','1')
insert into [SMS_News_Category]([CategoryName],[OrderID]) values('��ҵ����','2')
--select * from [SMS_News_Category]
go
-------------------------------------------------------------------------------
--����
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
insert into [SMS_News]([CategoryID],[Title],[Content]) values(1,'��ҵ����','��������')
insert into [SMS_News]([CategoryID],[Title],[Content]) values(2,'��ҵ����','��������')
--select * from [SMS_News]
go
-------------------------------------------------------------------------------
--��Ʒ����
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
insert into [SMS_Products_Category]([ParentID],[CategoryName],[OrderID]) values(0,'�ʼǱ�','1')
insert into [SMS_Products_Category]([ParentID],[CategoryName],[OrderID]) values(1,'����','1')
insert into [SMS_Products_Category]([ParentID],[CategoryName],[OrderID]) values(1,'��˶','2')
insert into [SMS_Products_Category]([ParentID],[CategoryName],[OrderID]) values(1,'����','3')
--select * from [SMS_Products_Category]
go
-------------------------------------------------------------------------------
--��Ʒ
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
insert into [SMS_Products]([CategoryID],[Name],[Picture],[Content]) values(2,'HP Pavilion ������ dv3-2100 ���ֱʼǱ�����ϵ��','200909141646025312795/o.jpg','HP Pavilion ������ dv3-2100 ���ֱʼǱ�����ϵ��')
insert into [SMS_Products]([CategoryID],[Name],[Picture],[Content]) values(3,'��˶ ��Ѷϵ�� X8AIN','200909141650077031848/o.jpg','��˶ ��Ѷϵ�� X8AIN')
insert into [SMS_Products]([CategoryID],[Name],[Picture],[Content]) values(4,'���� ���� K23','200909141653187656510/o.jpg','���� ���� K23')
--select * from [SMS_Products]
go
-------------------------------------------------------------------------------
--��Ƹ
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
insert into [SMS_Jobs]([Name],[Dept],[Email],[Address],[Num],[Sex],[Experience],[Language],[Salary],[Education],[ResumeLanguage],[Content]) values('����Ա','������','tsolong@126.com','�Ϻ�','10','��','2������','Ӣ��','����','����','����','��Ƹ����Ա')
--select * from [SMS_Jobs]
go
-------------------------------------------------------------------------------
--����
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
insert into [SMS_Leaveword]([Name],[Email],[Phone],[Content]) values('TsoLong','tsolong@126.com','13818791403','��վ����Ա����')
--select * from [SMS_Leaveword]
go
-------------------------------------------------------------------------------
--��������
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
--��������
insert into [SMS_Links]([SiteName],[SiteDomain],[LogoAddress],[LogoWidth],[LogoHeight]) values('TsoLong Blog','http://www.tsolong.com/','','','')
--ͼƬ����
insert into [SMS_Links]([SiteName],[SiteDomain],[LogoAddress],[LogoWidth],[LogoHeight]) values('TsoLong Blog','http://www.tsolong.com/','http://www.tsolong.com/other/logo.gif','88','31')
--select * from [SMS_Links]
go
-------------------------------------------------------------------------------
--ϵͳ�û�
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
--��ҳ�洢����
-------------------------------------------------------------------------------
if(object_id('DataPage ','p') is not null) drop procedure [DataPage]
go

create procedure [DataPage]
	@TableName nvarchar(100),		--���ݿ����
	@SelectColumnName nvarchar(1000) = '' ,	--��ѯ������
	@SelectWhere nvarchar(1500) = '',	--��ѯ������
	@OrderColumnName nvarchar(255) = '',	--������ֶ� (����Ϊ��)
	@OrderType bit = 0,			--�������� (0||false:���� ��0||true:����)
	@PageSize int = 10,			--ÿҳ��������
	@PageIndex int = 1,			--��ǰҳ��
	@RecordTotal int output 		--��¼����
as
set nocount on
declare @StrTotalSQL nvarchar(4000)
declare @StrSQL nvarchar(4000)
declare @StrTemp nvarchar(1000)
declare @StrOrder nvarchar(1000)
	
	--��¼����
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
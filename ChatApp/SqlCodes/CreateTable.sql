/*			TODO
	- user notifications (for=> re open group requests or system notifications or anything else)
*/


drop table if exists Notifications
create table Notifications(
id int primary key identity(1,1),
sender int not null,
receiver int not null,
title varchar(100) not null,
content varchar(1000) not null,
datetime datetime default CURRENT_TIMESTAMP,
readed int default 0
);


--drop table if exists Groups;
--create table Groups(
--id int primary key identity(1,1),
--token varchar(max) default NEWID(),
--owner int not null,
--name varchar(64) not null,
--global int not null,
--active int not null default 1
--);


--drop table if exists GroupsOfUser;
--create table GroupsOfUser(
--id int primary key identity(1,1),
--groupId int not null,
--userId int not null,
--);

--drop table if exists Users;
--create table Users(
--id int primary key identity(1,1),
--token varchar(max) default NEWID(),
--name varchar(64) default 'New User',
--email varchar(64) not null,
--password varchar(64) not null,
--active int default 1
--);


---------------------------------------------------------------------------

--insert into Messages (sender,content,chat) values (15,'here is the new message','439A3AE0-B99C-414B-A610-4774CEEC7017');
--select * From Messages;
--insert into Groups (name,global) values ('chat1',1);
--select * from Groups;
--select m.*,g.name from Messages as m join Groups as g on m.chat = g.token;
--insert into Users (name,email,password) values ('test1','test@gmail.com','asd123');
--select * from Users;
--insert into GroupsOfUser (groupId,userId) values (2,3),(4,3),(7,3);
--select * from GroupsOfUser where userId=3;
--select * from Groups as g join GroupsOfUser as gou on g.id=gou.groupId where gou.userId=3;

--delete from GroupsOfUser;

--insert into GroupsOfUser (groupId,userId) values (1,3);
--update Groups set name ='oda2' where id=2;

--select * from GroupsOfUser;
select * from Notifications;
--select * from Users; 
--select * from Notifications; 

--update Groups set global =0 where id=1;



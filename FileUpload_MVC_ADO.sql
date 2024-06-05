create procedure getAllEmployee
As 
begin
	select * from employee;
end;


exec getAllEmployee;


create procedure addEmployee
  @Name varchar(20),
    @Imagepath varchar(200),
    @Email varchar(50),
    @Department varchar(50)
as
begin
	insert into Employee (Name,ImagePath,Email,Department)
	values (@Name, @Imagepath,@Email,@Department);
end;
exec addEmployee 'Yadnesh','dfgdfgfdgdfvgdf','yadnesh@gmail.com','Account' ;


delete from employee;


create procedure [dbo].getEmployeeById
@Id int
as 
begin 
	select ID , Name,Imagepath,Email,Department 
	from Employee 
	where ID = @ID
end

exec getEmployeeById 4;



create procedure updateEmployee
@Id int,
  @Name varchar(20),
    @Imagepath varchar(200),
    @Email varchar(50),
    @Department varchar(50)
as
begin
	update Employee set 
					Name = @Name ,
					Imagepath = @Imagepath,
					Email = @Email,
					Department= @Department

	where ID = @Id  
end;



create procedure deleteEmployee
@Id int
as
begin
	delete from Employee
	where ID = @Id  
end;

exec deleteEmployee 5;
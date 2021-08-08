create proc InsertSP
@FirstName nvarchar(10),	
@LastName nvarchar(10),	
@Address nvarchar(50),	
@City nvarchar(10),	
@state nvarchar(10),	
@Zip int,
@PhoneNumber int, 
@Email nvarchar(10),
@AddressBookName nvarchar(10),	
@AddressBookType nvarchar(10)
as
begin
Insert into ContactInfo(FirstName,LastName,Address,City,State,Zip,PhoneNumber,Email,AddressBookName,AddressBookType)
values(@FirstName,@LastName,@Address,@City,@state,@Zip,@PhoneNumber,@Email,@AddressBookName,@AddressBookType)
end
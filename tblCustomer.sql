
create table tblCustomer(
Id  INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
customerID int not null,
FirstName varchar(50 ) not null,
Middle varchar(50),
LastName varchar(50) ,
KittaNo varchar(100),
CitizenshipNo int not null,
Address varchar(100) not null,
phoneNo varchar(10),
FatherName varchar(50) not null,
GrandFatherName varchar(50) not null,
EmailAddress varchar(50),
mobileNo varchar(20) ,
PhotocopyOfLalpurjaDoc varchar(max),
PhotocopyOfLalpurjaCode varchar(20),
TaxClearanceDoc varchar(max),
TaxClearanceCode varchar(20),
CitizenshipDoc varchar(max),
CitizenshipCode varchar(20),
NaapiNaksaWithKittaNoDoc varchar(max),
NaapiNaksaWithKittaNo varchar(50),
HouseDesginMapDoc varchar(max),
HouseDesginMapCode varchar(max),
)
create table tblApprovalCase1
(
ApprovalOfWardChairId INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
ApprovalOfWardChair bit ,
CustomerID int FOREIGN KEY REFERENCES tblCustomer(Id)
)
create table tblApprovalCase2
(
ApprovalOfWardChairLackOfAccessOfRoadId INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
ApprovalOfWardChairLackOfAccessOfRoad bit ,
CustomerID int FOREIGN KEY REFERENCES tblCustomer(Id)
)

create table Certification(
Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
IssueTemporayCertification bit,
PermamentCertification bit,
CompletionCertification bit,
CustomerID int FOREIGN KEY REFERENCES tblCustomer(Id)
)
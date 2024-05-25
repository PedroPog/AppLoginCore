create database LoginCore_M;
use LoginCore_M;

create table Cliente(
Id int primary key auto_increment,
Nome varchar(50) not null,
Nascimento datetime not null,
Sexo char(1),
CPF varchar(11) not null,
Telefone varchar(14) not null,
Email varchar(50) not null,
Senha varchar(8) not null,
Situacao char(1) not null
);

create table Colaborador(
Id int primary key auto_increment,
Nome varchar(50) not null,
Email varchar(50) not null,
Senha varchar(8) not null,
Tipo varchar(8) not null
);

insert into Cliente values(default, "Cliente01", "2000/01/01","M","12312312301","123456-123456","cliente@email.com","123456","A");

insert into Colaborador values
(default,"Gerente","gerente@email.com","123456","G"),
(default,"Colaborador","colaborador@email.com","123456","C");


create table tbLivro(
	idLivro int primary key auto_increment,
    nomeLivro varchar(50),
    imagemLivro varchar(255)
);

create table tbEmprestimo(
	idEmp int primary key auto_increment,
    dataEmp varchar(20),
    dataDev varchar(20),
    idCliente int references Cliente(Id)
);

create table itemsEmp(
	idItem int primary key auto_increment,
    idEmp int references tbEmprestimo(idEmp),
    idLivro int references tbLivro(idLivro)
);
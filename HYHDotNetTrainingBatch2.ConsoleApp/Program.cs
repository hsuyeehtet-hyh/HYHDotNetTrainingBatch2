﻿// See https://aka.ms/new-console-template for more information
using Microsoft.Data.SqlClient;
using System.Data;
using HYHDotNetTrainingBatch2.ConsoleApp;

Console.WriteLine("Hello, World!");


AdoDotNetExample adoDotNetExample = new AdoDotNetExample();
//adoDotNetExample.Read();
//adoDotNetExample.Create();
//adoDotNetExample.Update();

DapperExample dapperExample = new DapperExample();
//dapperExample.Read();
dapperExample.Edit();


Console.ReadKey();
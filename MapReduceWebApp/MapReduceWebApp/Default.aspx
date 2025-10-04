<%@ Page Title="Home" Language="C#" Async="true" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MapReduceWebApp._Default" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        .mapreduce-box {
            border: 1px solid #ccc;
            padding: 20px;
            width: 380px;
            margin: 30px auto;
            font-family: Arial;
        }

        .mapreduce-box h3 {
            margin-bottom: 15px;
            text-align: center;
        }

        .mapreduce-box label {
            display: block;
            margin-top: 10px;
        }

        .mapreduce-box input[type="text"], 
        .mapreduce-box input[type="number"] {
            width: 100%;
            padding: 5px;
            box-sizing: border-box;
        }

        .mapreduce-box .button-row {
            margin-top: 15px;
            display: flex;
            justify-content: space-between;
        }

        .mapreduce-box .button-row input,
        .mapreduce-box .button-row asp\:Button {
            width: 48%;
        }

        .mapreduce-box .full-button {
            width: 100%;
            margin-top: 10px;
        }

        .status-label {
            margin-top: 5px;
            font-weight: bold;
        }
    </style>

    <div class="mapreduce-box">
        <h3>Web Application Performing Automated MapReduce</h3>

        <div class="button-row">
            <asp:FileUpload ID="FileUpload1" runat="server" />
            <asp:Button ID="UploadButton" runat="server" Text="Upload" OnClick="UploadButton_Click" CssClass="btn" />

        </div>

        <div class="status-label">
            Status
            <span style="margin-left:10px; font-weight:bold;">
                <asp:Label ID="LabelUploadStatus" runat="server" ForeColor="Green" />
            </span>
        </div>

        <label>Choose N, the number of parallel threads. N >= 1</label>
        <asp:TextBox ID="TextBoxThreads" runat="server" Text="4" />

        <label>Provide the Web service address for Map function</label>
        <asp:TextBox ID="TextBoxMapUrl" runat="server" Text="https://localhost:44331/map" />

        <label>Provide the Web service address for Reduce function</label>
        <asp:TextBox ID="TextBoxReduceUrl" runat="server" Text="https://localhost:44308/reduce" />

        <label>Provide the Web service address for Combiner function</label>
        <asp:TextBox ID="TextBoxCombinerUrl" runat="server" Text="https://localhost:44339/combine" />

        <asp:Button ID="ButtonStart" runat="server" CssClass="full-button" Text="Perform MapReduce Computation" OnClick="ButtonStart_Click" />
        <asp:Literal ID="LiteralOutput" runat="server" />
    </div>

</asp:Content>


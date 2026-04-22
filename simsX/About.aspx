<%@ Page Title="" Language="C#" MasterPageFile="~/mstrSims.master" AutoEventWireup="true" CodeFile="About.aspx.cs" Inherits="About" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
        body {
            padding-top: 50px;
        }

        /*.vmidparent{
            height: 150px;
            line-height: 150px;
            text-align: center;
        }

        .vmidchild{
            display: inline-block;
            vertical-align: middle;
            line-height: normal;
        }*/

        .imgborder{
            border: 5px solid #3e8cbc;
        }

        .imgbordersims{
            border-color: #3e8dbd;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" Runat="Server">
    <asp:UpdatePanel runat="server" ID="upMain" UpdateMode="Conditional">
        <ContentTemplate>

            <div class="container container_content">

                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-primary">
                            <div class="box-body">
                                <div class="row">
                                    <div class="col-md-6">
                                        <asp:Image runat="server" ID="imgAboutIT" CssClass="img-responsive" ImageUrl="~/images/aboutIT.jpg" draggable="false"/>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:Image runat="server" ID="imgSIMSLOGO" CssClass="img-responsive center-block imgbordersims" Height="200px" BorderWidth="5px" BorderStyle="Solid" ImageUrl="~/images/simslogo.jpg" draggable="false"/>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12 vmidparent">
                                        <div class="text-justify vmidchild" style="font-size:medium">
                                            Statefields School Information Management System (SSIMS) is a web-based application software designed to introduce a conducive and structured information exchange environment for integrating students, teachers and the administration of Statefields School Inc.
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="box-footer">
                                <div class="col-md-6">
                                    <strong class="text-danger">IT MISSION STATEMENT</strong><br />
                                    <div class="text-justify">Information Technology Department provides secure, reliable, and integrated technology solutions in alignment with academic and administrative goals, while delivering excellence in customer service.</div>
                                </div>
                                <div class="col-md-6">
                                    <strong class="text-danger">IT VISION STATEMENT</strong><br />
                                    <div class="text-justify">Information Technology Department will be recognized as a high performance team providing technology excellence that advances learning, teaching, research, and student formation in alignment with Statefields School Inc.’s mission and goals.</div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-12">
                        <table class="table table-condensed">
                            <tr>
                                <td class="text-center">
                                    <asp:Image runat="server" CssClass="img-circle imgborder" BorderWidth="5px" Height="120px" ID="imgITHEAD"  onerror="this.onload = null; this.src='images/def_avatar/nophotoM.png';" draggable="false"/>
                                    <div class="row">
                                        <asp:Label runat="server" ID="lblITHEADNickName" Font-Bold="true"></asp:Label>
                                    </div>
                                    <div class="row">
                                        <asp:Label runat="server" ID="lblITHEADFullName" Font-Size="Large"></asp:Label>
                                    </div>
                                    <div class="row">
                                        <asp:Label runat="server" ID="lblITHEADPosition"></asp:Label>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-12 text-center">
                        <asp:Repeater ID="Repeater" runat="server">
                            <ItemTemplate>
                                <div class="col-md-4" style="margin-top:15px; margin-bottom:15px;">
                                    <asp:Image ID="imgITStaff" Height="100px" CssClass="img-circle imgborder" BorderWidth="5px" runat="server" ImageUrl='<%# Eval("ephotopath") %>' onerror="this.onload = null; this.src='images/def_avatar/nophotoM.png';" draggable="false"/>
                                    <div class="row">
                                        <asp:Label runat="server" ID="lblITSNickName" Font-Bold="true" Text='<%# Eval("NickName") %>'></asp:Label>
                                    </div>
                                    <div class="row">
                                        <asp:Label runat="server" ID="lblITSFullName" Font-Size="Large" Text='<%# Eval("FullName") %>'></asp:Label>
                                    </div>
                                    <div class="row">
                                        <asp:Label runat="server" ID="lblITSPosition" Text='<%# Eval("Position") %>'></asp:Label>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-primary">
                            <div class="box-header with-border text-center">
                                <strong style="font-size:x-large">CONTACT US</strong><br />
                            </div>
                            <div class="box-body">
                                <p class="text-center" style="font-size:medium">
                                    We would love to talk to you about any new PROJECTS you may have or to assist you with your concerns about our systems.
                                </p>
                                <br />
                                <div class="col-md-4 text-center">
                                    <asp:Image runat="server" CssClass="img-circle" Height="80px" ID="imgPhone" ImageUrl="~/images/phone.jpg" draggable="false"/>
                                    <div class="row">
                                        <span style="font-size:larger;font-weight:bold">Local 125</span>
                                    </div>
                                    <div class="row">
                                        <span style="font-size:medium;">IT Local Telephone Line</span>
                                    </div>
                                </div>
                                <div class="col-md-4 text-center">
                                    <asp:Image runat="server" CssClass="img-circle" Height="80px" ID="imgMail" ImageUrl="~/images/mail.jpg" draggable="false"/>
                                    <div class="row">
                                        <span style="font-size:larger;font-weight:bold">statefields@yahoo.com</span>
                                    </div>
                                    <div class="row">
                                        <span style="font-size:medium;">IT Official Email Address</span>
                                    </div>
                                </div>
                                <div class="col-md-4 text-center">
                                    <asp:Image runat="server" CssClass="img-circle" Height="80px" ID="imgSMP" ImageUrl="~/images/smp.jpg" draggable="false"/>
                                    <div class="row">
                                        <span style="font-size:larger;font-weight:bold">smp.mobileapp@statefields.edu.ph</span>
                                    </div>
                                    <div class="row">
                                        <span style="font-size:medium;">SMP Email Address</span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>


        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


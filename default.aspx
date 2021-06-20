<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Async="true" Inherits="TelerikWebAppResponsive.Default" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="Content0" ContentPlaceHolderID="head" Runat="Server">
    <link href="assets/styles/default.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <telerik:RadPageLayout runat="server" ID="RadPageLayout1">
        <Rows>
            <telerik:LayoutRow>
                <Columns>
                    <telerik:LayoutColumn>
                        <h3 class="title-align">Dashboard</h3>
                    </telerik:LayoutColumn>
                </Columns>
            </telerik:LayoutRow>
        </Rows>
    </telerik:RadPageLayout>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <telerik:RadPageLayout runat="server" ID="Content1">
        <Rows>
            <telerik:LayoutRow>
                <Columns>
                    <telerik:LayoutColumn Span="4" SpanMd="12" SpanSm="12"></telerik:LayoutColumn>
                    <telerik:LayoutColumn Span="4" SpanMd="12" SpanSm="12">
                        <a class="link-card" href="/TimeLine.aspx">
                            <div class="card-wrapper">
                                <div class="card">
                                    <h4>Moldes</h4>
                                    <p>Quantidade</p>
                                    <div class="quantity">
                                        <h1 id="mouldsQuantity" runat="server">17</h1>
                                        <i id="mouldsSpinner" runat="server" class="fa fa-spinner fa-spin" style="font-size:30px"></i>
                                    </div>
                                    <small class="see-more">Clique para mais detalhes</small>
                                </div>
                            </div>
                        </a>
                    </telerik:LayoutColumn>
                    <telerik:LayoutColumn Span="4" SpanMd="12" SpanSm="12"></telerik:LayoutColumn>

                    

                    <%--<telerik:LayoutColumn Span="4" SpanMd="12" SpanSm="12" >
                        <h4>H4 text font size 18 px.</h4>
                        <p><strong>Main content text font size 16px</strong>, aliquam turpis sed nisl mattis sagittis. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Ut vitae sapien metus. In hac habitasse platea dictumst. Aenean velit mauris, lobortis eu lacinia sed</p>

                        <p>Nullam facilisis neque ut aliquet imperdiet. Mauris ut odio augue. Curabitur in mi ac odio vestibulum lobortis. </p>
                        <telerik:RadButton runat="server" ID="RadButton3" Text="Button" ButtonType="SkinnedButton"></telerik:RadButton>
                    </telerik:LayoutColumn>--%>
                </Columns>
            </telerik:LayoutRow>
            <telerik:LayoutRow>
                <Columns>
                <telerik:LayoutColumn Span="6" SpanMd="12" SpanSm="12">
                    <a class="link-card" href="/PerformanceFrequency.aspx">
                        <div class="card-wrapper next-row">
                            <div class="card">
                                <h4>Desvios Detetados nos primeiros 10 Processos</h4>
                                <p style="margin-bottom: 12px;">Inductive Miner</p>
                                <div class="quantity">
                                    <telerik:RadGrid RenderMode="Lightweight" runat="server" ID="RadGridDesvios"></telerik:RadGrid>
                                    <i id="desviosSpinner" runat="server" class="fa fa-spinner fa-spin" style="font-size:30px"></i>
                                </div>
                                <small class="see-more">Clique para mais detalhes</small>
                            </div>
                        </div>
                    </a>
                </telerik:LayoutColumn>
                <telerik:LayoutColumn Span="6" SpanMd="12" SpanSm="12" >
                        <%--<a class="link-card" href="/PerformanceFrequency.aspx">--%>
                            <div class="card-wrapper next-row">
                                <div class="card">
                                    <h4>Média da Duração de Criação de Moldes por Processos</h4>
                                    <%--<p>Inductive Miner</p>--%>
                                    <telerik:RadHtmlChart runat="server" ID="RadHtmlChart1" Height="400px">
                                        <PlotArea>
                                            <Series>
                                                <%--<telerik:ColumnSeries></telerik:ColumnSeries>--%>
                                            </Series>  
                                        </PlotArea>
                                        <Legend>
                                            <Appearance Position="Bottom">
                                            </Appearance>
                                        </Legend>
                                        <ChartTitle Text="Duração média para a criação de um molde em Horas">
                                            <Appearance Position="Top">
                                            </Appearance>
                                        </ChartTitle>
                                    </telerik:RadHtmlChart> 
                                    <div class="quantity" id="graphSpinner" runat="server">
                                        <i class="fa fa-spinner fa-spin" style="font-size:30px"></i>
                                    </div>
                                </div>
                            </div>
                        <%--</a>--%>
                    </telerik:LayoutColumn>
                </Columns>
            </telerik:LayoutRow>
        </Rows>
    </telerik:RadPageLayout>
    <p style="text-align:end;margin-bottom:-20px;" id="dadosAtualizados" runat="server" ></p>
</asp:Content>

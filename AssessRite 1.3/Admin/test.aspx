<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin.Master" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="AssessRite_1._3.Admin.test" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="table-responsive">
        <table id="example" class="table table-bordered" cellspacing="0" width="100%">
            <thead>
                <tr>
                    <th>Customer ID
                    </th>
                    <th>Details
                    </th>
                    <th>Name
                    </th>
                    <th>Title
                    </th>
                    <th>City
                    </th>

                </tr>
            </thead>
            <tfoot>
            </tfoot>
        </table>
         <table class="table table-bordered" id="myTable">
                            <thead>
                                <tr>
                                    <th style="display: none">ObjectiveId</th>
                                    <th style="display: none">ConceptId</th>
                                    <th style="display: none">SubjectId</th>
                                    <th style="display: none">ClassId</th>
                                    <th>ClassName</th>
                                    <th>SubjectName</th>
                                    <th>ConceptName</th>
                                    <th>ObjectiveName</th>
                                     <th>Details</th>
                                    <th>Delete</th>
                                </tr>
                            </thead>
                        </table>
    </div>
    <script>
       
        $(document).ready(function () {
            loadtable();
           
        });
        $(function () {
            var table = $('#example').DataTable
            ({
                "ajax": { "url": "jsonArray.txt" },
                "responsive": true,
                "sPaginationType": "full_numbers",
                //"columnDefs": [{ "targets": 1, "data": null, "defaultContent": "<input id='btnDetails' class='btn btn-success' width='25px' value='Get Details' />" }]
                columnDefs: [{
                    // puts a button in the last column
                    targets: [1], render: function (a, b, data, d) {
                        //if (data.Title == 'Owner') {
                       // alert(data.name);
                        return "<input id='btnDetails' class='btn btn-success' width='25px' value='Get Details' />";
                        //}
                        //return "";
                    }
                }]
            });

            $('#example tbody').on('click', '[id*=btnDetails]', function () {
                var data = table.row($(this).parents('tr')).data();
                var customerID = data[0];
                var name = data[1];
                var title = data[2];
                var city = data[3];
                alert("Customer ID : " + customerID + "\n" + "Name : " + name + "\n" + "Title : " + title + "\n" + "City : " + city);
            });
        });

        var table1;
        function loadtable() {
            $.ajax({
                type: "POST",
                url: "/WebMethods/GetData.asmx/GetObjectiveData",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    // console.log(data.d)
                    var json = JSON.parse(data.d);
                    table1 = $('#myTable').DataTable({
                        data: json,
                        columns: [
        { className: "hide", data: 'ObjectiveId' },
        { className: "hide", data: 'ConceptId' },
         { className: "hide", data: 'SubjectId' },
         { className: "hide", data: 'ClassId' },
          
        { data: 'ClassName' },
        { data: 'SubjectName' },
        { data: 'ConceptName' },
        { data: 'ObjectiveName' },
        //{ data: null }
        //{
        //    mRender: function (data, type, row) {
        //        return '<input id="" class="btn-link" value="View Concept" onclick="View(' + row.ObjectiveId + ')"/> | <a  class="btn-link" onclick="Delete(' + row.ObjectiveId + ')">View Question Paper</a>'
        //    }
        //}
        {
            data: "ObjectiveName", render: function (data, type, row) {
                return '<a  class="btn-link" onclick="Delete(' + row.ObjectiveId + ')">View Question Paper</a>'
            }
        },
        {
            data: "ObjectiveName", render: function (data, type, row) {
                return '<input id="" class="btn-link" value="View Concept" onclick="View(' + row.ObjectiveId + ')"/>'
            }
        }
      //{
      //    data: "ConceptName", render: function (data, type, row) {
      //        return '<input id="btnViewConcepts" class="btn-link lnk" onclick="View(' + row.ConceptName + ');" style="width:100px"  value="View Concept"/>'
      //    }
      //}
                        ]
                        
                        //columnDefs: [{
                        //    // puts a button in the last column
                        //    targets: [-1], render: function (a, b, data, d) {
                        //        //if (data.SubjectName == 'Maths') {
                        //        return "<input id='btnDetails1' class='btn-link' width='25px' value='"+data.SubjectName+"' />";
                        //       // }
                        //      //  return "";
                        //    }
                        //}]
                    });
                    //$('#myTable tbody tr').on('click', '[id*=btnDetails1]', function () {
                    //   // alert("working");
                    //    var concept = $(this).val();
                    
                    //});
                }
            });
           
        }
        function Delete(objectiveid)
        {
            alert("workingQ:" + objectiveid);
            return false;
        }
        function View(ConceptName) {
            alert("workingV:" + ConceptName);
            return false;
        }
       
    </script>
</asp:Content>

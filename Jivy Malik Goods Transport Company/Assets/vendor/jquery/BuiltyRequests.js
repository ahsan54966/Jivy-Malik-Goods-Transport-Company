document.write('<scr' + 'ipt type="text/javascript" src="~/Assets/vendor/jquery/jquery-1.10.2.js" ></scr' + 'ipt>');
document.write('<scr' + 'ipt type="text/javascript" src="~/Assets/vendor/jquery/jquery-ui.js" ></scr' + 'ipt>');
       $(document).ready(function () {
           $("#Builty_BuiltyTruckNumber").autocomplete({
               source: function (request, response) {
                   $.ajax({
                       url: '@Url.Action("GetVehicles", "Admin")',
                       datatype: "json",
                       data: {
                           Prefix: request.term
                       },
                       success: function (data) {
                           response($.map(data, function (val, item) {
                               return {
                                   label: val.VehicleNumber,
                                   value: val.VehicleNumber
                               }
                           }))
                       }
                   })
               },
               messages: {
                   noResults: "", results: ""
               }
           });
       });



/*
$(document).ready(function () {
    $("#Builty_BuiltyTruckNumber").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "/Admin/GetVehicles",
                type: "POST",
                dataType: "json",
                data: { Prefix: request.term },
                success: function (data) {
                    response($.map(data, function (item) {
                        return { label: item.VehicleNumber, value: item.VehicleNumber };
                    }))

                }
            })
        },
        messages: {
            noResults: "", results: ""
        }
    });
});





//For Builty DropDown of Vehicle Number
$(document).ready(function () {
    $("#Builty_BuiltyTruckNumber").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: '@Url.Action("GetVehicles", "Admin")',
                datatype: "json",
                data: {
                    Prefix: request.term
                },
                success: function (data) {
                    response($.map(data, function (val, item) {
                        return {
                            label: val.VehicleNumber,
                            value: val.VehicleNumber
                        }
                    }))
                }
            })
        },
            messages: {
        noResults: "", results: ""
            }
    });
});



/*
$(document).ready(function () {
        $("#BuiltyVehicleNumber").autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: "/Admin/VehicleNumber",
                    type: "POST",
                    dataType: "json",
                    data: { Prefix: request.term },
                    success: function (data) {
                        response($.map(data, function (item) {
                            return { label: item.VehicleNumber, value: item.VehicleNumber };
                        }))

                    }
                })
            },
            messages: {
                noResults: "", results: ""
            }
        });
        $("#BuiltyVehicleNumber").focusout(function () {
            var VehicleNumber = $("#BuiltyVehicleNumber").val();
            $.ajax({
                url: "/Admin/GetVehicleInformation",
                type: "POST",
                dataType: "json",
                data: { VehicleNumber: VehicleNumber },
                success: function (data) {
                    console.log(data);
                    console.log(data.DriverName);
                    $("#DriverName").val(data.DriverName);
                    $("#PhoneNumber").val(data.PhoneNumber);
                    $("#VehicleSize").val(data.VehicleSize);
                }
            })
            console.log(CityName);
        });

    })

*/

// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
//Populate fields Register Form
//Button1
function PopulateFieldBtn1() {


    $("#dwCommitteeSel").val(2);
    $("#dwSalutation").val("Mr.");

    $("#txtAddress").val("3 Church Street");
    $("#txtCity").val("St Catharines");
    $("#dwProvince").val("ON").change();
    $("#txtPostalCode").val("L2R 3E2");
    $("#txtTelephoneNumber").val("1234567811");
    $("#txtPassword").val("User123$");
    $("#txtConfirmPass").val("User123$");
    $("#txtPositionTitle").val("Computer Programmer");
    $("#txtCompanyName").val("My Company");
    $("#txtCompanyAddress").val("100 Church Street");
    $("#txtCompanyCity").val("St Catharines");
    $("#dwCompanyProvince").val("ON").change();
    $("#txtCompanyPostalCode").val("L2R 3E2");
    $("#txtCompanyTelephoneNumber").val("1234567855");
    $("#txtCompanyEmail").val("clark@company.com");
    $("#rdYes").prop("checked", true);
    $("#rdPersonal").prop('checked', true);
    /*$("#rdActive").prop("checked", true);*/
    $("#txtEducationSummary").val("Computer Programming, Leadership Development");
    $("#txtOccupationalSumary").val("Write programs in a variety of computer languages, such as C++ and Java,Update and expand existing programs");
}


function PopulateFieldBtn2() {

    $("#dwCommitteeSel").val(2);
    $("#dwSalutation").val("Mr.");

    $("#txtAddress").val("25 York St");
    $("#txtCity").val("St Catharines");
    $("#dwProvince").val("ON").change();
    $("#txtPostalCode").val("L2R 5R7");
    $("#txtTelephoneNumber").val("1234562276");
    $("#txtPassword").val("User123$");
    $("#txtConfirmPass").val("User123$");
    $("#txtPositionTitle").val("Computer Programmer");
    $("#txtCompanyName").val("My Company 2");
    $("#txtCompanyAddress").val("100 Welland Avenue");
    $("#txtCompanyCity").val("St Catharines");
    $("#dwCompanyProvince").val("NS").change();
    $("#txtCompanyPostalCode").val("L2R 3E2");
    $("#txtCompanyTelephoneNumber").val("1234567888");
    $("#txtCompanyEmail").val("wayne@company.com");
    $("#rdYes").prop("checked", true);
    $("#rdPersonal").prop('checked', true);
    /*$("#rdActive").prop("checked", true);*/
    $("#txtEducationSummary").val("Programmer");
    $("#txtOccupationalSumary").val("Mobile applications");
}


function PopulateFieldBtn3() {

    $("#dwCommitteeSel").val(2);
    $("#dwSalutation").val("Mr.");

    $("#txtAddress").val("75 Yates St");
    $("#txtCity").val("St Catharines");
    $("#dwProvince").val("ON").change();
    $("#txtPostalCode").val("L2R 5R7");
    $("#txtTelephoneNumber").val("12345678");
    $("#txtPassword").val("User123$");
    $("#txtConfirmPass").val("User123$");
    $("#txtPositionTitle").val("Computer Programmer");
    $("#txtCompanyName").val("My Company 3");
    $("#txtCompanyAddress").val("23 Church Street");
    $("#txtCompanyCity").val("St Catharines");
    $("#dwCompanyProvince").val("ON").change();
    $("#txtCompanyPostalCode").val("L2R 3E2");
    $("#txtCompanyTelephoneNumber").val("1234567");
    $("#txtCompanyEmail").val("erling@company.com");
    $("#rdPersonal").prop('checked', true);
    /*$("#rdActive").prop("checked", true);*/
    $("#txtEducationSummary").val("QA");
    $("#txtOccupationalSumary").val("Testing area");
}


/*Clear data fields*/
function ClearFields() {

    $("#dwCommitteeSel").val(null);
    $("#dwSalutation").val(null);

    $("#txtAddress").val("");
    $("#txtCity").val("");
    $("#dwProvince").val(null);
    $("#txtPostalCode").val("");
    $("#txtTelephoneNumber").val("");
    $("#txtPassword").val("");
    $("#txtConfirmPass").val("");
    $("#txtPositionTitle").val("");
    $("#txtCompanyName").val("");
    $("#txtCompanyAddress").val("");
    $("#txtCompanyCity").val("");
    $("#dwCompanyProvince").val(null);
    $("#txtCompanyPostalCode").val("");
    $("#txtCompanyTelephoneNumber").val("");
    $("#txtCompanyEmail").val("");
    $("#rdYes").prop("checked", false);
    $("#rdNo").prop("checked", false);
    $("#rdPersonal").prop('checked', false);
    $("#rdWork").prop('checked', false);
    //$("#rdActive").prop("checked", false);
    //$("#rdInactive").prop("checked", false);
    $("#txtEducationSummary").val("");
    $("#txtOccupationalSumary").val("");
}
//TODO : Fix confirmation
//$("#btnRegister").click(function () {
//    if($("#test").val() != ""){
//        swal({
//            title: "Something went wrong",
//            text: "Please review your form",
//            icon: "error",
//            timer: "7000",
//        });
//    }
//    else {
//        swal({
//            title: "Thank you!",
//            text: "Form submitted!",
//            icon: "success",
//            timer: "7000",
//        });
//    }
//});

////Confirmation for submitted form Register page
//$("#btnRegister").click(function () {
//    if ($("#txtFirstName").val() == "" || $("#txtLastName").val() == "" || $("#txtEmail").val() == "") {
//        swal({
//            title: "Something went wrong",
//            text: "Please review your form",
//            icon: "error",
//            timer: "7000",
//        });
//    }
//    else {
//        swal({
//            title: "Thank you!",
//            text: "Form submitted!",
//            icon: "success",
//            timer: "7000",
//        });
//    }
//});

//Show or hide information for Active chk input in Volunteers maintain table
if ($('#chkActive').click(function () {
    if ($(this).is(":checked")) {
        $("#txtActiveLegend").prop('hidden',false)
        $("#txtActiveLegendElse").prop('hidden',true);
    } else {
        $("#txtActiveLegendElse").prop('hidden',false)
        $("#txtActiveLegend").prop('hidden',true);
    }
}));
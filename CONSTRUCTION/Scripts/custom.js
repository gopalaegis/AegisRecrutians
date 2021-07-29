
$(document).ready(function () {    
    $(".preloader").fadeOut();
});


function LoadTableHeader(ID)
{
    
    $('#' + ID + '_length').wrapAll('<div class="table-head" id="' + ID + '_Header"><div class="head1"></div></div>');
    $('#' + ID + '_length').removeClass('dataTables_length').addClass('entries');
    $('#' + ID + '_filter').insertAfter('#' + ID +'_length');
    $('#' + ID + '_filter').removeClass('dataTables_filter').addClass('search-bar');
   
}
function LoadTableapplicantHeader(ID) {

    $('#' + ID + '_length').wrapAll('<div class="table-headdata" id="' + ID + '_Header"><div class="head1"></div></div>');
    $('#' + ID + '_length').removeClass('dataTables_length').addClass('entries');
    $('#' + ID + '_filter').insertAfter('#' + ID + '_length');
    $('#' + ID + '_filter').removeClass('dataTables_filter').addClass('search-bar');

}
function OnLogOut() {

    $.ajax({
        url: '/User/UserLogin/LogOut',
        type: 'GET',
        async: false,
        cache: false,
        contentType: false,
        processData: false,
        success: function (result) {

           

            window.location.href = '/Home/Index';






        }
    });


}
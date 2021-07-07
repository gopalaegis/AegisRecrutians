$(document).ready(function () {
    var readURL = function (input) {

        $(".preloader").fadeIn();
        if (input.files && input.files[0]) {
            var reader = new FileReader();

            reader.readAsDataURL(input.files[0]);
            /*Validation of File*/
            var file = input.files[0];
            var sFileName = file.name;
            var sFileExtension = sFileName.split('.')[sFileName.split('.').length - 1].toLowerCase();
            var iFileSize = file.size;
            var iConvert = (file.size / 1048576).toFixed(2);


            if (!(sFileExtension === "pdf" ||
                sFileExtension === "doc" ||
                sFileExtension === "docx") || iConvert > 2) { /// 10 mb
                var msg = "File type : " + sFileExtension + "\n\n";
                msg += "Size: " + iConvert + " MB \n\n";
                msg += "Please make sure your file is in pdf or doc format and less than 2 MB.\n\n";
                $(".preloader").fadeOut();
                alert(msg);
                return false;
            }
            else {
                var formdata = new FormData();
                formdata.append(input.files[0].name, input.files[0]);

                $.ajax({
                    url: '/Profile/ResumeUpload',
                    type: 'POST',
                    data: formdata,
                    async: false,
                    cache: false,
                    contentType: false,
                    enctype: 'multipart/form-data',
                    processData: false,
                    success: function (data) {
                        $(".preloader").fadeOut();
                        if (data.isSuccess == true) {
                            swal({
                                title: "Success",
                                text: data.message,
                                icon: "success",
                            })
                            location.reload();



                        }
                        else {

                            swal("Oops", data.message, "error");
                        }


                    }
                });
            }

        }
    };
    $(".uploadResume").on("change", function () {

        readURL(this);
    });

    //var Element = "divjobfilterdiv";
    //$('#' + Element + " .accordion").each(function () {



    //    //this.classList.toggle("active");
    //    var panel = this.nextElementSibling;
    //    if (panel.style.maxHeight) {
    //        panel.style.maxHeight = null;
    //    }
    //    else {
    //        panel.style.maxHeight = panel.scrollHeight + "px";
    //    }



    //});

    $("#PR1").css({
        "background-color": "#ddd"
    });
});


$(document).ready(function () {
    var readURL = function (input) {
        if (input.files && input.files[0]) {
            var reader = new FileReader();
            reader.onload = function (e) {
                $(".profile-pic").attr("src", e.target.result);
            };

            reader.readAsDataURL(input.files[0]);
            var formdata = new FormData();
            formdata.append(input.files[0].name, input.files[0]);


            $.ajax({
                url: '/Profile/Upload',
                type: 'POST',
                data: formdata,
                async: false,
                cache: false,
                contentType: false,
                enctype: 'multipart/form-data',
                processData: false,
                success: function (data) {
                    if (data.isSuccess == true) {
                        swal({
                            title: "Success",
                            text: data.message,
                            icon: "success",
                        })
                        var url = '/Profile/Index';

                        window.location.href = url;



                    }
                    else {

                        swal("Oops", data.message, "error");
                    }


                },
                error: function (data, xhr, resp) {
                    console.log(xhr);
                },
            });
        }
    };
    $(".file-upload").on("change", function () {
        readURL(this);
    });
    $(".upload-button").on("click", function () {
        $(".file-upload").click();
    });
});


   




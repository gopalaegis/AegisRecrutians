/*------------------------------------
    Project Name: Project Name

    [Table of Content]

    ## Document Ready
        - Aegis Navigation
        - Menu Responsive Caret
        - Sticky Navigation
        - Banner Slider

---------------------------------------*/

(function($) {

    "use strict"

    /* + Responsive Caret */
    function menu_dropdown_open() {
        var width = $(window).width();
        if ($(".menu-navigation .navigation-menu li.ddl-active").length) {
            if (width > 991) {
                $(".menu-navigation .navigation-menu > li").removeClass("ddl-active");
                $(".menu-navigation .navigation-menu li .dropdown-menu").removeAttr("style");
            }
        } else {
            $(".menu-navigation .navigation-menu li .dropdown-menu").removeAttr("style");
        }
    }

    /* + Expand Panel Resize */
    function panel_resize() {
        var width = $(window).width();
        if (width > 991) {
            if ($(".header_s .slidepanel").length) {
                $(".header_s .slidepanel").removeAttr("style");
            }
        }
    }

    /* ## Document Ready */
    $(document).on("ready", function() {

        /* - Aegis Navigation */
        $(".menuswitch").on("click", function() {
            $(".navigation-menu").toggleClass("active");
        });

        /* - Menu Responsive Caret */
        $(".ddl-switch").on("click", function() {
            var li = $(this).parent();
            if (li.hasClass("ddl-active") || li.find(".ddl-active").length !== 0 || li.find(".dropdown-menu").is(":visible")) {
                li.removeClass("ddl-active");
                li.children().find(".ddl-active").removeClass("ddl-active");
                li.children(".dropdown-menu").slideUp();
            } else {
                li.addClass("ddl-active");
                li.children(".dropdown-menu").slideDown();
            }
        });

        /* - Sticky Navigation */
        var nav = $('.header_s');
        $(window).scroll(function() {
            if ($(this).scrollTop() > 55) {
                nav.addClass("navigation-fixed animated fadeInDown");
            } else {
                nav.removeClass("navigation-fixed animated fadeInDown");
            }
        });
        /*===== go to top ===========*/
        $(window).scroll(function () {
            if ($(this).scrollTop() >= 100) {
                $('#return-to-top').fadeIn(200);
            } else {
                $('#return-to-top').fadeOut(200);
            }
        });
        $('#return-to-top').click(function () { $('body,html').animate({ scrollTop: 0 }, 500); });
        // ========= right-side fixed form
        $("#inquiry_form").click(function () {
            $(".b-notification-bar").toggleClass("active");
            if ($(".b-notification-bar .inq").hasClass('info-box1')) {
                $(".b-notification-bar .inq").removeClass("info-box1");
                $("#inquiry_form").removeClass("contact")
            } else {
                $(".b-notification-bar .inq").addClass("info-box1");
                $("#inquiry_form").addClass("contact")
            }
            $("#rfp_form").removeClass("submitrfp");
            $(".b-notification-bar .rfp").removeClass("info-box2")
        });
        $(".closeInquiry").click(function () {
            $(".b-notification-bar").removeClass("active");
            $(".b-notification-bar .inq").removeClass("info-box1");
            $("#inquiry_form").removeClass("contact")
        });
        $(".closeRfp").click(function () {
            $(".b-notification-bar").removeClass("active");
            $(".b-notification-bar .rfp").removeClass("info-box2");
            $("#rfp_form").removeClass("submitrfp")
        });
        $("#rfp_form").click(function () {
            $(".b-notification-bar").toggleClass("active");
            $(".b-notification-bar .inq").removeClass("info-box1");
            $("#inquiry_form").removeClass("contact");
            if ($(".b-notification-bar .rfp").hasClass("info-box2")) {
                $(".b-notification-bar .rfp").removeClass("info-box2");
                $("#rfp_form").removeClass("submitrfp")
            } else {
                $(".b-notification-bar .rfp").addClass("info-box2");
                $("#rfp_form").addClass("submitrfp")
            }
        });
        /* - Banner Slider */
        // $('.bxslider').bxSlider({
        //     auto: true,
        //     autoControls: true,
        //     speed: 2000,
        // });

        /* - Magnific Popup ===== */
         if($(".product-image").length){
            var url;
            $(".product-image").magnificPopup({
                delegate: "a.zoom",
                type: "image",
                tLoading: "Loading image #%curr%...",
                mainClass: "mfp-img-mobile",
                gallery: {
                    enabled: true,
                    navigateByImgClick: false,
                    preload: [0,1] // Will preload 0 - before current, and 1 after the current image
                },
                image: {
                    tError: "<a href="%url%">The image #%curr%</a> could not be loaded.",               
                }
            });
        } 
        
        /* - TimeLine */
        var timelineBlocks = $('.cd-timeline-block'),
            offset = 0.8;

        //hide timeline blocks which are outside the viewport
        hideBlocks(timelineBlocks, offset);

        //on scolling, show/animate timeline blocks when enter the viewport
        $(window).on('scroll', function() {
            (!window.requestAnimationFrame) ? setTimeout(function() {
                showBlocks(timelineBlocks, offset);
            }, 100): window.requestAnimationFrame(function() {
                showBlocks(timelineBlocks, offset);
            });
        });

        function hideBlocks(blocks, offset) {
            blocks.each(function() {
                ($(this).offset().top > $(window).scrollTop() + $(window).height() * offset) && $(this).find('.cd-timeline-img, .cd-timeline-content').addClass('is-hidden');
            });
        }

        function showBlocks(blocks, offset) {
            blocks.each(function() {
                ($(this).offset().top <= $(window).scrollTop() + $(window).height() * offset && $(this).find('.cd-timeline-img').hasClass('is-hidden')) && $(this).find('.cd-timeline-img, .cd-timeline-content').removeClass('is-hidden').addClass('bounce-in');
            });
        }

    }); /* - Document Ready /- */

    /* + Window On Resize */
    $(window).on("resize", function() {
        var width = $(window).width();
        var height = $(window).height();
        menu_dropdown_open();
    });    

    $(".why_aegis #solutions-box").owlCarousel({        
        autoplay: true,
        // margin: 30,
        lazyLoad: true,
        // items: 3,
        loop: true,
        responsiveClass: true,
        autoplayTimeout: 7000,
        nav: false,
        navText: [""],
        dots: true,
        responsive: {
            0: {
                items: 1,
                nav: false
            },
            600: {
                items: 2,
                nav: false
            },
            1024: {
                items: 3,
                nav: false
            }
        }
    });
    //=========== OWL Product Category
    $(document).ready(function() {
        $(".owl-carousel").owlCarousel();
    });
    
    // Add active class to the current button (highlight it)
    $(document).ready(function() {
        $('.navigation-menu li a').click(function() {
            $('li a').removeClass("active");
            $(this).addClass("active");
        });
    });

    /*==============================================================
        smooth scroll
    ==============================================================*/

    var scrollAnimationTime = 1200,
        scrollAnimation = 'easeInOutExpo';
    $(document).on('click.smoothscroll', 'a.scrollto', function(event) {
        event.preventDefault();
        var target = this.hash;
        if ($(target).length != 0) {
            $('html, body').stop()
                .animate({
                    'scrollTop': $(target)
                        .offset()
                        .top
                }, scrollAnimationTime, scrollAnimation, function() {
                    window.location.hash = target;
                });
        }
    });

    // ========= Parallax effect
    $(window).scroll(function(e) {
        parallax();
    })

    function parallax() {
      var scroll = $(window).scrollTop();
      var screenHeight = $(window).height();

      $('.parallax').each(function() {
        var offset = $(this).offset().top;
        var distanceFromBottom = offset - scroll - screenHeight
        
        if (offset > screenHeight && offset) {
          $(this).css('background-position', 'center ' + (( distanceFromBottom  ) * 0.1) +'px');
        } else {
          $(this).css('background-position', 'center ' + (( -scroll ) * 0.1) + 'px');
        }
      })
    };
    /*======= tabs =======*/
    const tabLinks = document.querySelectorAll(".tabs a");
    const tabPanels = document.querySelectorAll(".tabs-panel");

    for (let el of tabLinks) {
        el.addEventListener("click", (e) => {
            e.preventDefault();

            document.querySelector(".tabs li.active").classList.remove("active");
            document.querySelector(".tabs-panel.active").classList.remove("active");

            const parentListItem = el.parentElement;
            parentListItem.classList.add("active");
            const index = [...parentListItem.parentElement.children].indexOf(parentListItem);

            const panel = [...tabPanels].filter((el) => el.getAttribute("data-index") == index);
            panel[0].classList.add("active");
        });
    }
    /*========= User Profile photo upload ===========*/
    $(document).ready(function () {
        var readURL = function (input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $(".profile-pic").attr("src", e.target.result);
                };
                reader.readAsDataURL(input.files[0]);
            }
        };
        $(".file-upload").on("change", function () {
            readURL(this);
        });
        $(".upload-button").on("click", function () {
            $(".file-upload").click();
        });
    });

    // ========== Accordion
    var acc = document.getElementsByClassName("accordion");
    var i;

    for (i = 0; i < acc.length; i++) {
      acc[i].addEventListener("click", function() {
        this.classList.toggle("active");
        var panel = this.nextElementSibling;
        if (panel.style.maxHeight) {
          panel.style.maxHeight = null;
        } else {
          panel.style.maxHeight = panel.scrollHeight + "px";
        } 
      });
    }

    /*======== show more ========= */
    $(".moreless-button").click(function () {
        $(".moretext").slideToggle();
        if ($(".moreless-button").text() == "22 More") {
            $(this).text("+ 22 More");
        } else {
            $(this).text("Read less");
        }
    });

    // Dropdown job-sorting
    $('.show_list .dropdown-toggle').click(function(){
        $(this).next('.search_sorting .dropdown').slideToggle("fast");
    });
    // Hide dropdown on page click job-sorting
    $(document).on('click', function (e) {
        if(!$(".show_list .dropdown-toggle").is(e.target) && !$(".show_list .dropdown-toggle").has(e.target).length){
            $('.search_sorting .dropdown').slideUp("fast");
        }                       
    });

    /*============ Ajax Auto ============*/
    /*http://laravel.io/forum/02-08-2014-ajax-autocomplete-input*/
    var countries = {
        "AD": "Andorra",
        "A2": "Andorra Test",
        "AE": "United Arab Emirates",
        "AF": "Afghanistan",
        "AG": "Antigua and Barbuda",
        "AI": "Anguilla",
        "AL": "Albania",
        "AM": "Armenia",
        "AN": "Netherlands Antilles",
        "AO": "Angola",
        "AQ": "Antarctica",
        "AR": "Argentina",
        "AS": "American Samoa",
        "AT": "Austria",
        "AU": "Australia",
        "AW": "Aruba",
        "AX": "\u00c5land Islands",
        "AZ": "Azerbaijan",
        "BA": "Bosnia and Herzegovina",
        "BB": "Barbados",
        "BD": "Bangladesh",
        "BE": "Belgium",
        "BF": "Burkina Faso",
        "BG": "Bulgaria",
        "BH": "Bahrain",
        "BI": "Burundi",
        "BJ": "Benin",
        "BL": "Saint Barth\u00e9lemy",
        "BM": "Bermuda",
        "BN": "Brunei",
        "BO": "Bolivia",
        "BQ": "British Antarctic Territory",
        "BR": "Brazil",
        "BS": "Bahamas",
        "BT": "Bhutan",
        "BV": "Bouvet Island",
        "BW": "Botswana",
        "BY": "Belarus",
        "BZ": "Belize",
        "CA": "Canada",
        "CC": "Cocos [Keeling] Islands",
        "CD": "Congo - Kinshasa",
        "CF": "Central African Republic",
        "CG": "Congo - Brazzaville",
        "CH": "Switzerland",
        "CI": "C\u00f4te d\u2019Ivoire",
        "CK": "Cook Islands",
        "CL": "Chile",
        "CM": "Cameroon",
        "CN": "China",
        "CO": "Colombia",
        "CR": "Costa Rica",
        "CS": "Serbia and Montenegro",
        "CT": "Canton and Enderbury Islands",
        "CU": "Cuba",
        "CV": "Cape Verde",
        "CX": "Christmas Island",
        "CY": "Cyprus",
        "CZ": "Czech Republic",
        "DD": "East Germany",
        "DE": "Germany",
        "DJ": "Djibouti",
        "DK": "Denmark",
        "DM": "Dominica",
        "DO": "Dominican Republic",
        "DZ": "Algeria",
        "EC": "Ecuador",
        "EE": "Estonia",
        "EG": "Egypt",
        "EH": "Western Sahara",
        "ER": "Eritrea",
        "ES": "Spain",
        "ET": "Ethiopia",
        "FI": "Finland",
        "FJ": "Fiji",
        "FK": "Falkland Islands",
        "FM": "Micronesia",
        "FO": "Faroe Islands",
        "FQ": "French Southern and Antarctic Territories",
        "FR": "France",
        "FX": "Metropolitan France",
        "GA": "Gabon",
        "GB": "United Kingdom",
        "GD": "Grenada",
        "GE": "Georgia",
        "GF": "French Guiana",
        "GG": "Guernsey",
        "GH": "Ghana",
        "GI": "Gibraltar",
        "GL": "Greenland",
        "GM": "Gambia",
        "GN": "Guinea",
        "GP": "Guadeloupe",
        "GQ": "Equatorial Guinea",
        "GR": "Greece",
        "GS": "South Georgia and the South Sandwich Islands",
        "GT": "Guatemala",
        "GU": "Guam",
        "GW": "Guinea-Bissau",
        "GY": "Guyana",
        "HK": "Hong Kong SAR China",
        "HM": "Heard Island and McDonald Islands",
        "HN": "Honduras",
        "HR": "Croatia",
        "HT": "Haiti",
        "HU": "Hungary",
        "ID": "Indonesia",
        "IE": "Ireland",
        "IL": "Israel",
        "IM": "Isle of Man",
        "IN": "India",
        "IO": "British Indian Ocean Territory",
        "IQ": "Iraq",
        "IR": "Iran",
        "IS": "Iceland",
        "IT": "Italy",
        "JE": "Jersey",
        "JM": "Jamaica",
        "JO": "Jordan",
        "JP": "Japan",
        "JT": "Johnston Island",
        "KE": "Kenya",
        "KG": "Kyrgyzstan",
        "KH": "Cambodia",
        "KI": "Kiribati",
        "KM": "Comoros",
        "KN": "Saint Kitts and Nevis",
        "KP": "North Korea",
        "KR": "South Korea",
        "KW": "Kuwait",
        "KY": "Cayman Islands",
        "KZ": "Kazakhstan",
        "LA": "Laos",
        "LB": "Lebanon",
        "LC": "Saint Lucia",
        "LI": "Liechtenstein",
        "LK": "Sri Lanka",
        "LR": "Liberia",
        "LS": "Lesotho",
        "LT": "Lithuania",
        "LU": "Luxembourg",
        "LV": "Latvia",
        "LY": "Libya",
        "MA": "Morocco",
        "MC": "Monaco",
        "MD": "Moldova",
        "ME": "Montenegro",
        "MF": "Saint Martin",
        "MG": "Madagascar",
        "MH": "Marshall Islands",
        "MI": "Midway Islands",
        "MK": "Macedonia",
        "ML": "Mali",
        "MM": "Myanmar [Burma]",
        "MN": "Mongolia",
        "MO": "Macau SAR China",
        "MP": "Northern Mariana Islands",
        "MQ": "Martinique",
        "MR": "Mauritania",
        "MS": "Montserrat",
        "MT": "Malta",
        "MU": "Mauritius",
        "MV": "Maldives",
        "MW": "Malawi",
        "MX": "Mexico",
        "MY": "Malaysia",
        "MZ": "Mozambique",
        "NA": "Namibia",
        "NC": "New Caledonia",
        "NE": "Niger",
        "NF": "Norfolk Island",
        "NG": "Nigeria",
        "NI": "Nicaragua",
        "NL": "Netherlands",
        "NO": "Norway",
        "NP": "Nepal",
        "NQ": "Dronning Maud Land",
        "NR": "Nauru",
        "NT": "Neutral Zone",
        "NU": "Niue",
        "NZ": "New Zealand",
        "OM": "Oman",
        "PA": "Panama",
        "PC": "Pacific Islands Trust Territory",
        "PE": "Peru",
        "PF": "French Polynesia",
        "PG": "Papua New Guinea",
        "PH": "Philippines",
        "PK": "Pakistan",
        "PL": "Poland",
        "PM": "Saint Pierre and Miquelon",
        "PN": "Pitcairn Islands",
        "PR": "Puerto Rico",
        "PS": "Palestinian Territories",
        "PT": "Portugal",
        "PU": "U.S. Miscellaneous Pacific Islands",
        "PW": "Palau",
        "PY": "Paraguay",
        "PZ": "Panama Canal Zone",
        "QA": "Qatar",
        "RE": "R\u00e9union",
        "RO": "Romania",
        "RS": "Serbia",
        "RU": "Russia",
        "RW": "Rwanda",
        "SA": "Saudi Arabia",
        "SB": "Solomon Islands",
        "SC": "Seychelles",
        "SD": "Sudan",
        "SE": "Sweden",
        "SG": "Singapore",
        "SH": "Saint Helena",
        "SI": "Slovenia",
        "SJ": "Svalbard and Jan Mayen",
        "SK": "Slovakia",
        "SL": "Sierra Leone",
        "SM": "San Marino",
        "SN": "Senegal",
        "SO": "Somalia",
        "SR": "Suriname",
        "ST": "S\u00e3o Tom\u00e9 and Pr\u00edncipe",
        "SU": "Union of Soviet Socialist Republics",
        "SV": "El Salvador",
        "SY": "Syria",
        "SZ": "Swaziland",
        "TC": "Turks and Caicos Islands",
        "TD": "Chad",
        "TF": "French Southern Territories",
        "TG": "Togo",
        "TH": "Thailand",
        "TJ": "Tajikistan",
        "TK": "Tokelau",
        "TL": "Timor-Leste",
        "TM": "Turkmenistan",
        "TN": "Tunisia",
        "TO": "Tonga",
        "TR": "Turkey",
        "TT": "Trinidad and Tobago",
        "TV": "Tuvalu",
        "TW": "Taiwan",
        "TZ": "Tanzania",
        "UA": "Ukraine",
        "UG": "Uganda",
        "UM": "U.S. Minor Outlying Islands",
        "US": "United States",
        "UY": "Uruguay",
        "UZ": "Uzbekistan",
        "VA": "Vatican City",
        "VC": "Saint Vincent and the Grenadines",
        "VD": "North Vietnam",
        "VE": "Venezuela",
        "VG": "British Virgin Islands",
        "VI": "U.S. Virgin Islands",
        "VN": "Vietnam",
        "VU": "Vanuatu",
        "WF": "Wallis and Futuna",
        "WK": "Wake Island",
        "WS": "Samoa",
        "YD": "People's Democratic Republic of Yemen",
        "YE": "Yemen",
        "YT": "Mayotte",
        "ZA": "South Africa",
        "ZM": "Zambia",
        "ZW": "Zimbabwe",
        "ZZ": "Unknown or Invalid Region"
    }
    var countriesString = [
        "Andorra",
        "Andorra Test",
        "United Arab Emirates"
    ];
    var countriesArray = $.map(countries, function (value, key) { return { value: value, data: key }; });

    // Initialize ajax autocomplete:
    $('.autocomplete').autocomplete({
        // serviceUrl: '/autosuggest/service/url',
        //lookup: countriesString,
        lookup: countriesArray,
        lookupFilter: function(suggestion, originalQuery, queryLowerCase) {
            var re = new RegExp('\\b' + $.Autocomplete.utils.escapeRegExChars(queryLowerCase), 'gi');
            return re.test(suggestion.value);
        },
        onSelect: function(suggestion) {
            $('#selction-ajax').html('You selected: ' + suggestion.value + ', ' + suggestion.data);
        },
        onHint: function (hint) {
            $('#autocomplete-ajax-x').val(hint);
        },
        onInvalidateSelection: function() {
            $('#selction-ajax').html('You selected: none');
        }
    });

    /* Fully year */
    var date = new Date().getFullYear();document.getElementById("year").innerHTML = date;
    
})(jQuery);
    AOS.init({once: true,});
/*--- other doc ----*/
$(".pin").click(function () {
  $("#other-doc").trigger('click');
});

$('#other-doc').on('change', function() {
  var val = $(this).val();
  $(this).siblings('span').text(val);
});

/*======= Profile Modals =======*/
// Get the modal
        var modalparent = document.getElementsByClassName("modal_multi");
        // Get the button that opens the modal
        var modal_btn_multi = document.getElementsByClassName("myBtn_multi");
        // Get the <span> element that closes the modal
        var span_close_multi = document.getElementsByClassName("close_multi");
        // When the user clicks the button, open the modal
        function setDataIndex() {
            for (i = 0; i < modal_btn_multi.length; i++) {
                modal_btn_multi[i].setAttribute("data-index", i);
                modalparent[i].setAttribute("data-index", i);
                span_close_multi[i].setAttribute("data-index", i);
            }
        }
        for (i = 0; i < modal_btn_multi.length; i++) {
            modal_btn_multi[i].onclick = function () {
                var ElementIndex = this.getAttribute("data-index");
                modalparent[ElementIndex].style.display = "block";
            };
            // When the user clicks on <span> (x), close the modal
            span_close_multi[i].onclick = function () {
                var ElementIndex = this.getAttribute("data-index");
                modalparent[ElementIndex].style.display = "none";
            };
        }
        window.onload = function () {
            setDataIndex();
        };
        window.onclick = function (event) {
            if (event.target === modalparent[event.target.getAttribute("data-index")]) {
                modalparent[event.target.getAttribute("data-index")].style.display = "none";
            }
            // OLD CODE
            // if (event.target === modal) {
            //     modal.style.display = "none";
            // }
        }; 

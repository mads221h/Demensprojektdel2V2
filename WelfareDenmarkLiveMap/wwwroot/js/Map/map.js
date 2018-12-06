/*regionValues = {
     "0661": 'col1',
     "0420": 'col1',
     "0230": 'col1'
     };

     regionScale = {
     "col1": "#96bf31",
     "col2": "#003366",
     "col3": "#000066",
     "col4": "#006600",
     "col5": "#8864ab"
     };
     */

/*dataKommuner = {
 "0661": "Vestforsyning"
 }*/

//console.log("Values", arrRegionValues);

//jQuery.noConflict();


jQuery(document).ready(function () {
    var $ = jQuery;

    var zoomTimer = "";

    /*	$('#focus-single').click(function(){
     $('#map1').vectorMap('set', 'focus', 'DK-83');
     });
     $('#focus-multiple').click(function(){
     //$('#map1').vectorMap('set', 'focus', ['DK-84']);
     $('#map1').vectorMap('set', 'focus', 4.8, .57, .61);
     });
     $('#focus-init').click(function(){
     $('#map1').vectorMap('set', 'focus', 1, 0, 0);
     });*/

    var mouseX = 0;
    var mouseY = 0;

    // Gets mouse coordinates on click
    $('#flexkort_3FQK8').on('click', function (e) {
        mouseX = e.pageX - this.offsetLeft;
        mouseY = e.pageY - this.offsetTop;
        //console.log(mouseX, mouseY);
    });

    var mapConfigJSON = {
        map: 'denmark_county_merc_en',
        backgroundColor: 'transparent',
        zoomOnScroll: true,
        zoomButtons: true,
        regionsSelectable: true, // Changed to true - André Gollubits
        regionLabelStyle: {
            initial: {
                fill: '#444',
                'font-size': '9',
                'font-weight': 'normal'

            },
            hover: {
                fill: 'black'
            }
        },
        labels: {
            regions: {
                render: function (code) {
                    return "";
                }
            }
        },
        regionStyle: {
            initial: {
                fill: '#879693',
                "fill-opacity": 1,
                stroke: '#ffffff',
                "stroke-width": 1,
                "stroke-opacity": 1
            },
            hover: {
                fill: '#c6da35',
                "fill-opacity": 1,
                "stroke-width": 2

            },
            selected: {
                fill: '#c6da35'
            },
            selectedHover: {}
        },
        // custom farver:
        series: {
            regions: [{
                values: arrRegionValues,
                scale: arrRegionScales

            }]
        },
        onMarkerTipShow: function (e, label, code) {
            console.log("onMarkerTipShow");
            label.html('Anything you want');
            //or do what you want with label, it's just a jQuery object
        },
        onRegionClick: function (event, code) {
            /*console.log("click", code);
            console.log("click", arrLinks[code]);*/


        },
        onRegionOver: function (event, code) {
            //console.log(event, code);
            setTimeout(function () {
                color = "";
                scale = "";

                if (typeof arrRegionValues != 'undefined' && arrRegionValues) {
                    scale = arrRegionValues[code];
                }

                if (scale && typeof arrRegionScalesOver != 'undefined') {
                    color = arrRegionScalesOver[scale];
                }

                if (color) {
                    $("path[data-code=" + code + "]").attr("fill", color);
                }
            }, 10)
        },

        onRegionSelected: function (event, label, code) {
            //var mapObject = $('#kortKommuner').vectorMap('get', 'mapObject');
            //$("#debug").html("Valgte regioner: " + mapObject.getSelectedRegions());
            var e = this;
            if ($("#county-" + label).length) {
                $("#county-" + label).remove();
                return;
            }

            $.ajax({
                url: '/map/data',
                type: 'Post',
                dataType: 'json',
                data: { countyID: label },
                success: function (data) {
                    console.log(data);
                    $(e).append("<div id='county-" + label + "' class='statistics'>" +
                        "<span class='county-name'>" + data["Name"] + "</span>" +
                        "<span class='number-of-patients'><i class='fab fa-accessible-icon'></i> " + data["Patient-count"] + "</span>" +
                        "<span class='average-completetion-rate'><i class='fas fa-check-square'></i> " + data["Completion-avg"].toFixed(2) + "%</span>" +
                        "</div > ");
                    $("#county-" + label).css({ "left": mouseX, "top": mouseY });
                    //$(".statistics").draggable();
                },
                error: function (error) {
                    alert(error);
                }
            });
        },
        onRegionTipShow: function (event, label, code) {
            //console.log("Show tip start.");
            if (typeof arrInfoboxData != 'undefined' && arrInfoboxData[code]) {
                label.html(
                    '<div class="infobox" style="background: ' + arrBaseConfig['infoboxColor'] + '"><b>' + label.html() + '</b><br /><br />' +
                    '' + arrInfoboxData[code] + '' +
                    '</div>'
                );
            } else {
                label.html("<div class='inner'>" + label.html() + '</div>');
            }
        },
        onViewportChange: function (event, scale) {
            if (zoomTimer) clearTimeout(zoomTimer);
            zoomTimer = setTimeout(function () {
                console.log(scale);
                if (scale > 1.01) {
                    $(".jvectormap-reset").fadeIn();
                } else {
                    $(".jvectormap-reset").fadeOut();
                }
            }, 100)


        }
    }
    if (arrBaseConfig['useLegend'] == 1) {
        mapConfigJSON.series.regions[0].legend = {
            horizontal: true,
            //vertical: true,
            title: '',
            labelRender: function (v) {
                return arrLegend[v];
                /*return {
                 redGreen: 'low',
                 yellowBlue: 'high'
                 }[v];*/
            }
        }
    }



    flexMap = $('#flexkort_3FQK8').vectorMap(mapConfigJSON)

});
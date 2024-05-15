
var calendar;
GetCalendar(0);
debugger
function GetCalendar(TeamId) {
    debugger
    var view = window.location.search;
    if (view.includes('isMonthView')) view = 'dayGridMonth';
    else if (view.includes('isWeekView')) view = 'resourceTimelineWeek';
    else view = 'resourceTimeline';
    var calendarEl = document.getElementById('calendar');
    calendar = new FullCalendar.Calendar(calendarEl, {
        height: 'auto',
        schedulerLicenseKey: 'GPL-My-Project-Is-Open-Source',
        themeSystem: 'bootstrap5',
        headerToolbar: false,
        initialView: view,
        eventDisplay: 'block',
        //using datesSet we set data inside html tags in view
        datesSet: function (info) {
            console.log(info);
            if (info.view.type === 'resourceTimeline') {
                // Get the date object for the current view
                const date = info.view.calendar.getDate();

                // Format the date (e.g., Monday, 2024-04-08)
                const formattedDate = new Intl.DateTimeFormat('en-US', { weekday: 'long', year: 'numeric', month: 'short', day: 'numeric' }).format(date);

                // Update the calendar title
                document.getElementById("calendarTitle").innerHTML = formattedDate;
                document.querySelector('.fc-datagrid-cell-main').textContent = "STAFF";

            }
            else if (info.view.type === 'resourceTimelineWeek') {
                document.getElementById("calendarTitle").innerHTML = info.view.title;
                document.querySelector('.fc-datagrid-cell-main').textContent = "STAFF"

            }
            else {
                // Handle other view types (optional)
                // For example, you could display a different title for month or week views
                document.getElementById("calendarTitle").innerHTML = info.view.title;
                //document.querySelector('.fc-datagrid-cell-main').textContent = "STAFF"
            }
        },
        eventTimeFormat: {
            hour: 'numeric',
            minute: '2-digit',
            meridiem: 'short'
        },
        eventClick: function (info, jsEvent, view) {
            ViewShift(info.event.id);
        },
        views: {

            resourceTimeline: {
                //    slotLabelContent: function (slotInfo) {
                //        var hours = slotInfo.date.getHours();
                //        var meridiem = hours >= 12 ? 'P' : 'A';
                //        hours = hours % 12 || 12;

                //        return hours + meridiem;
                //    },
                //    width: "100%",
                slotDuration: '01:00:00'
            },
            resourceTimelineWeek: {

                slotDuration: { days: 1 },
                slotLabelFormat: { weekday: 'short', day: 'numeric', week: 'long' }
            },
        },
        //resourceLabelDidMount: function (resourceObj) {
        //    const img = document.createElement('img');
        //    img.src = resourceObj.resource.extendedProps.imageUrl || "/images/doc-ico.png";
        //    img.style.maxHeight = '40px';
        //    img.style.margin = '2px';
        //    resourceObj.el.querySelector('.fc-datagrid-cell-main').appendChild(img);
        //},
        resources: "/TeamLead/GetTeamMembersData?TeamId=" + TeamId,
        events: "/TeamLead/GetTaskData?TeamId=" + TeamId,
    });
    calendar.render();
}
let calendarNext = () => calendar.next();
let calendarPrev = () => calendar.prev();
let calendarToday = () => calendar.today();
let changeView = function (type) {
    calendar.changeView(type);
    switch (type) {
        case 'dayGridMonth':
            var url = new URL(window.location.href);
            url.search = '';
            url.searchParams.set('isMonthView', true);
            window.history.pushState({ path: url.href }, '', url.href);
            break;
        case 'resourceTimelineWeek':
            var url = new URL(window.location.href);
            url.search = '';
            url.searchParams.set('isWeekView', true);
            window.history.pushState({ path: url.href }, '', url.href);
            break;
        case 'resourceTimeline':
            var url = new URL(window.location.href);
            url.search = '';
            url.searchParams.set('isDayView', true);
            window.history.pushState({ path: url.href }, '', url.href);
            break;
    }
}


var team = "<option value=\"0\" selected>All Teams</option>";
$.ajax({
    url: `/TeamLead/GetTeams`,
    type: "GET",
    success: function (data) {
        console.log(data);
        $.each(data, function (i, obj) {
            team += "<option value=\"" + obj.teamId + "\">" + obj.name + "</option>";
        })
        console.log(team);
        $("#SelectedTeam").html(team);
    },
    error: function (error) {
        console.error(error);
        alert("error occured");
    }
})
//$("#Region").on("change", function () {
//    var RegionId = $(this).val();
//    GetCalendar(RegionId);
//   console.log("onchange func of region")
//})

document.getElementById("SelectedTeam").addEventListener("change", function () {
    var TeamId = this.value;
    console.log(TeamId);
    GetCalendar(TeamId);

});


function ViewShift(ShiftDetailId) {
    $.ajax({
        type: "GET",
        url: "/Admin/ViewShift",
        data: { ShiftDetailId },
        success: function (data) {
            $("#ViewShift").html(data);
            $("#ViewShiftModal").modal("show");
        },
        error: function (data) {
            alert("Cannot Fetch Shift Details");
        }
    })

}

function loadShifts() {
    $.ajax({
        url: '/TeamLead/GetTaskData',
        /*data: { RegionId: $("#ManagerId").val() },*/
        type: 'GET',
        success: function (data) {
            calendar.removeAllEvents();
            data.forEach(function (event) {
                calendar.addEvent(event);
            });
        },
        error: function (xhr, status, error) {
            alert('Error fetching schedule data:', error);
        }
    });
}

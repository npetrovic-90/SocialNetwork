//concert controller
var ConcertsController = function (attendanceService) {

	var button;

	var init = function (container) {
		//attendances toggle
		$(container).on("click", ".js-toggle-attendance", toggleAttendance);
		
	};

	//manager method
	var toggleAttendance = function (e) {

		button = $(e.target);
		var concertId = button.attr("data-concert-id");

		if (button.hasClass("btn-outline-dark"))
			attendanceService.createAttendance(concertId, done, fail);
		else
			attendanceService.deleteAttendance(concertId, done, fail);

	};



	//done method
	var done = function () {
		var text = (button.text() == "Going") ? "Going?" : "Going";

		button.toggleClass("btn-info").toggleClass("btn-outline-dark").text(text);
	}

	var fail = function () {
		alert("Something failed");
	}
	//fail method
	return {
		init: init
	}

}(AttendanceService);
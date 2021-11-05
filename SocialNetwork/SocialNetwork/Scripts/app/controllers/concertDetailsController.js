var ConcertDetailsController = function (followingService) {
    var followButton;

    //init
    var init = function () {
         //toggle following
        $(".js-toggle-follow").click(toggleFollowing);
    };

    //manager method
   
    var toggleFollowing = function (e) {

        followButton = $(e.target);

        var followeeId = followButton.attr("data-user-id");

        if (followButton.hasClass("btn-outline-dark"))
            followingService.createFollowing(followeeId, done, fail);
        else
            followingService.deleteFollowing(followeeId, done, fail);
    };

    //done method
    var done = function () {
        var text = (followButton.text() == "Follow") ? "Following" : "Follow";
        followButton.toggleClass("btn-info").toggleClass("btn-outline-dark").text(text);
    }

    //fail method
    var fail = function () {
        alert("Something failed");
    }

    return {
        init:init
    }

}(FollowingService);
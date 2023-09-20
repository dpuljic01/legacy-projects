$(function(){
    var screenWidth = window.innerWidth;
    var rectWidth = screenWidth / 5;
    var rectHeight = 30;
    var playerWidth = rectWidth / 2;
    var topPosition = -30,
        gameOver = false,
        isDown = false,
        elements = [],
        speed = 2,
        score = 0,
        step = rectWidth * 0.1,
        goLeft = $(".left"),
        goRight = $(".right"),
        player,
        rand;
    
    var rectStyles = {
        position: 'absolute',
        borderRadius: '5px',
        zIndex: 1,
        top: topPosition + 'px',
        width: rectWidth + 'px',
        height: rectHeight + 'px'
    };

    var requestAnimationFrame = window.requestAnimationFrame || window.mozRequestAnimationFrame || window.webkitRequestAnimationFrame || window.msRequestAnimationFrame;
     
    // 5 possible left positions for rect.
    var leftPositions = [0, rectWidth, rectWidth * 2, rectWidth * 3, rectWidth * 4];
    
    var isPhone =
    /Mobi/i.test(navigator.userAgent) || /Android/i.test(navigator.userAgent);
    
    // MAIN CALL
    $(".start").click(start);
    $(window).resize(resized);

    function resized() {
        screenWidth = window.innerWidth;
        rectWidth = screenWidth / 5;
        playerWidth = rectWidth / 2;
        step = rectWidth * 0.1;
        leftPositions = [0, rectWidth, rectWidth * 2, rectWidth * 3, rectWidth * 4]
        rectStyles = {
            position: 'absolute',
            borderRadius: '5px',
            zIndex: 1,
            top: topPosition + 'px',
            width: rectWidth + 'px',
            height: rectHeight + 'px'
        };
        $(".rect").css(rectStyles);
        player.css({ 
            width: playerWidth,
            left: screenWidth / 2 - playerWidth / 2,
            top: window.innerHeight - 35
        });
        $(".rect").each(function(i) {
            $(this).css('left', leftPositions[i]);
        });
    }
    
    function start() {
        $(".player, .rect").remove();
        $(".scoring").addClass("anim");
        $(".start").fadeOut();
        init();
        move();
        if (isPhone) phoneMove();
        else pcMove();
    }

    function phoneMove() {
        goLeft.on("touchstart", function() {
            isDown = true;
        });
        goRight.on("touchstart", function() {
            isDown = true;
        });

        $(document).on("touchend", function() {
             if (isDown) isDown = false;
        });
    
        goLeft.on("touchstart", function(event) {
             if (!gameOver) movePlayer("-=" + step);
        });
        goRight.on("touchstart", function(event) {
            if (!gameOver) movePlayer("+=" + step);
        });
    }

    function pcMove() {
        $(document).bind("keydown", function(event) {
            var key = event.which;
            if (key == 37) movePlayer("-=20");
            if (key == 39) movePlayer("+=20");
        });
    }
    
    function movePlayer(step){
        $(".player").animate({
            'left': step +'px'
        }, 5, function(){
            if (isDown){
                movePlayer(step);
            }
            if(parseInt($(".player").css("left")) <= 0) {
            isDown = false;
            $(".player").css("left", '0');
        }
        if(parseInt($(".player").css("left")) >= screenWidth - playerWidth) {
            isDown = false;
            $(".player").css("left", screenWidth - playerWidth);
        }
        });
    }
    
    function init() {
        speed = 2;
        score = 0;
        topPosition = -30;
        gameOver = false;
        $(".score").text(score);
    
        for(var i = 0; i < 5; i++) {
            elements[i] = ($('<div class="rect"></div>'));
            $("body").append(elements[i]);
        }
        $(".rect").css(rectStyles);
        $(".rect").css('background-color', randomColor());
        
        rand = random(0, 4);
        $(".rect").each(function(i, el) {
            if(i == rand){ 
                $(el).hide(); 
                return;
            }
            $(el).css('left', leftPositions[i]);
        });
        initPlayer();
    }
    
    function initPlayer() {
        player = $('<div class="player"></div>');
        player.css({
            position: 'absolute',
            width: playerWidth,
            borderRadius: '5px',
            height: rectHeight,
            backgroundColor: '#222',
            top: window.innerHeight - 35,
            left: screenWidth / 2 - playerWidth / 2
        });
        player.text("😎");
        $("body").append(player);
    }
    
    function move() {
        topPosition += speed;     
        if(topPosition > window.innerHeight - rectHeight) {
            updateParameters();
        }
        $(".rect").css({top: topPosition + 'px'});
        gameOver = collided();
        if(!gameOver) {
            requestAnimationFrame(move);
        }
        else {
            isDown = false;
            $(document).unbind("keydown");
            player.text("🤕");
            $(".scoring").removeClass("anim");
            $(".start").val("Restart").fadeIn();
        }
    }
    
    function updateParameters() {
        rand = random(0, 4);
        $(".rect").each(function(i, el) {
            if(i == rand){ 
                $(el).hide();
                return;
            }
            $(el).show();
            $(el).css('left', leftPositions[i]);
        });
        $(".rect").css('background-color', randomColor());
        
        topPosition = -30;
        speed += 0.1;
        score += 10;
        $(".score").text(score);
        $(".score").animate({
            fontSize:"1.6em"
        }, 200).animate({
            fontSize:"1.2em"
        }, 200)
    }
    
    function collided() {
        var leftBorder = leftPositions[rand];
        var rightBorder = leftBorder + rectWidth;
        var playerLeft = parseInt($(".player").css("left"));
        var playerRight = playerLeft + playerWidth;
        
        //just in case if player goes a bit out of screen there is 50px margin for that
        if(leftBorder == 0) {
            leftBorder = -50;
        }
        if(leftBorder == leftPositions[4]) {
            rightBorder  = leftBorder + rectWidth + 50;
        }
        if(topPosition > window.innerHeight - 65){
            if(playerLeft > leftBorder && playerRight < rightBorder) {
                return false;
            }
            else {
                return true;
            }
        }
    }
    
    function randomColor() {
        var r = random(0, 240);
        var g = random(0, 240);
        var b = random(0, 240);
        return 'rgb(' + r + ', ' + g + ', ' + b + ')';
    }
    
    function random(min, max) {
        return Math.floor(Math.random() * (max - min + 1) + min);
    }
});
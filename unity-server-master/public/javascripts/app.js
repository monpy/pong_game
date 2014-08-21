$(function(){
  var $up_button = $('#up_button');
  var $down_button = $('#down_button');
  var $notice = $('#notice');
  var $enemy = $('#enemy');
  var $controller = $('#controller');
  var socket = io.connect();
  var nowY = 0;
  var h = $(window).height();

  var mc = new Hammer(document.getElementById('controller'));
  mc.on("pan panstart panmove", function(ev) {
    nowY = ev.center.y;
  });

  mc.on("panend", function(ev) {
    nowY = 0;
  });

  var gameTimer = setInterval(function(){
    if (nowY == 0) {
      return;
    }

    if (nowY < h / 2){
      console.log('up');
      socket.emit('up');
    }else{
      console.log('down');
      socket.emit('down');
    }
  },1000/60);

  $up_button.on('click', function() {
    console.log('up');
    socket.emit('up');
  });

  $down_button.on('click', function() {
    console.log('down');
    socket.emit('down');
  });

  socket.on('connect', function() {
    $notice.show();
    $notice.html('サーバーと接続しました');
    $notice.delay(300).fadeOut('300', function() {});
  });

  socket.on('enemy', function(mes) {
    console.log('enemy:', mes);
    $enemy.append('<div>' + mes + '</div>');
  });

  socket.on('newPlayer', function() {
    $notice.show();
    $notice.html('新しい敵が入りました');
    $notice.fadeOut('300', function() {});
  });

  socket.on('otherDisconnect', function() {
    $notice.html('敵が切断しました');
  });

  socket.on('disconnect', function() {
    $notice.html('サーバーから切断されました');
  });
});

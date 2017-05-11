<?php
//데이터베이스 접속
$db_host = "localhost";
$db_user = "root";
$db_passwd="asdf";
$db_name = "ROOM";

//데이터 베이스 연결 저장
$conn = mysqli_connect($db_host,$db_user,$db_passwd,$db_name);

//POST 방식으로 전달된 room number 저장
$room_num = $_POST["room_num"];

$sql = "SELECT MAX(game_num) FROM ".$room_num;
$result = mysqli_query($conn,$sql);
$maxgame_num = mysqli_fetch_array($result);
$max_game_num= $maxgame_num['MAX(game_num)'];


// $i 는 문제 수를 나타낸다
for($i =1; $i<=$max_game_num ;$i++)
{

  //같은 문제 번호 안에서 맞춘사람 id를 나열 timestamp순으로 정렬하는 query
  $sql= "SELECT * FROM ".$room_num." WHERE game_num = ".$i." AND answ= 1 ORDER BY answ_time";
//  echo $sql; echo "<br>";

  $result = mysqli_query($conn,$sql);
/*  if($result)
  {
    echo "game_num = ".$i." 인 행들을 TIMESTAMP 순서로 select 했습니다"; echo "<br>";
  }
  else {
    echo "game_num select 에러"; echo "<br>";
  }*/

  // 출력 table의 row의 갯수
  $tabrow_num = mysqli_num_rows($result)*10;
  //echo "출력 table 의 row 갯수: ".$tabrow_num; echo "<br>";

  //row 카운트 변수
  $count = 0;
  //할당 점수는 맞춘 사람들의 수와 비례하여 순위가 낮을 수록 -1 씩 차등지급
  while($row = mysqli_fetch_array($result)){
    $add_score =$tabrow_num-$count;
    $sql = "UPDATE ".$room_num;
    $sql .= "\n SET answ= ".$add_score;
    $sql .= "\n WHERE id = ".$row["id"];
  //  echo $sql ; echo "<br>";

    $update = mysqli_query($conn,$sql);
  /*  if($update)
    {
      echo "game_num = ".$i." 인 행들 중 순위 ".$count." 위의 점수 차증 분배가 update 되었습니다"; echo "<br>";
    }
    else {
      echo "game_num 점수 update 에러"; echo "<br>";
    }*/
    $count=$count+5;
  }
}


 ?>

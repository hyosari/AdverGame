<?php
//데이터베이스 접속
$db_host = "localhost";
$db_user = "root";
$db_passwd="asdf";
$db_name = "ROOM";

//데이터 베이스 연결 저장
$conn = mysqli_connect($db_host,$db_user,$db_passwd,$db_name);

/*if(mysqli_connect_errno($conn)){
  echo "데이터 베이스 연결 실패: " . mysqli_connect_errno();
  echo " <br>";
}else {
  echo "야호! 성공 ";
  echo "<br>";
}*/

//POST 방식으로 전달된 room number 저장
$room_num = $_POST["room_num"];

//$ex_room = "HYO_123";

// $i 는 문제 수를 나타낸다
for($i =1; $i<2 ;$i++)
{

  //같은 문제 번호 안에서 맞춘사람 id를 나열 timestamp순으로 정렬하는 query
  $sql= "SELECT * FROM ".$room_num." WHERE game_num = ".$i." AND answ= 1 ORDER BY answ_time";
 // echo $sql; echo "<br>";

  $result = mysqli_query($conn,$sql);
 /* if($result)
  {
    echo "game_num = ".$i." 인 행들을 TIMESTAMP 순서로 select 했습니다"; echo "<br>";
  }
  else {
    echo "game_num select 에러"; echo "<br>";
  }*/

  // 출력 table의 row의 갯수
  $tabrow_num = mysqli_num_rows($result);
  //echo "출력 table 의 row 갯수: ".$tabrow_num; echo "<br>";

  //row 카운트 변수
  $count = 0;
  //할당 점수는 맞춘 사람들의 수와 비례하여 순위가 낮을 수록 -1 씩 차등지급
  while($row = mysqli_fetch_array($result)){
    $add_score =$tabrow_num-$count;
    $sql = "UPDATE ".$room_num;
    $sql .= "\n SET answ= ".$add_score;
    $sql .= "\n WHERE id = ".$row["id"];
    //echo $sql ; echo "<br>";

    $update = mysqli_query($conn,$sql);
   /* if($update)
    {
      echo "game_num = ".$i." 인 행들 중 순위 ".$count." 위의 점수 차증 분배가 update 되었습니다"; echo "<br>";
    }
    else {
      echo "game_num 점수 update 에러"; echo "<br>";
    }*/
    $count++;
  }
}

  //update가 끝났으면 학번별로 정렬하여 answ의 값들을 다 더한다
  $sql = "CREATE TABLE ".$room_num."_Rank AS";
  $sql .= "\n SELECT stu_num, SUM(answ) as score  FROM ".$room_num." GROUP BY stu_num";
  //echo $sql; echo "<br>";

  //rank table 을 만듬
  $result = mysqli_query($conn,$sql);
  if(!$result){
    echo " RANK Table 생성 에러"; echo "<br>";
    exit(" 해당 ROOM에 대한 Rank 테이블이 이미 존재합니다. 삭제하시고 다시 시도해 주세요 ");	
  }
  $sql="SELECT* FROM ".$room_num."_Rank ORDER BY score desc ";
  $result = mysqli_query($conn,$sql);

 /*if($result)
  {
    echo "학번별로 점수 합산이 완료되었습니다"; echo "<br>";
  }
  else {
    echo "점수 합산 에러"; echo "<br>";
  }*/

  //JSON 생성을 위한 배열
  $rows = array();
  $return = array();

  //QUery의 결곽밧을 처움부터 마지막까지 진행하면서 추출
  while ($row = mysqli_fetch_array($result)) {
    $rows["stu_num"] = $row["stu_num"]; //학번 추출
    $rows["score"] =(int) $row["score"]; // 합산점수


    //JSON 데이터 생성을 위한 배열에 레코드 값 추가
    array_push($return, $rows);
    }

    header("Content-type:application/json");
    //json파일 생성
    echo json_encode($return);
    //DB접속 종료
    mysqli_close($conn);





 ?>

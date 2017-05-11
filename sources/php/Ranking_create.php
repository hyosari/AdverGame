<?php
//데이터베이스 접속
$db_host = "localhost";
$db_user = "root";
$db_passwd="asdf";
$db_name = "ROOM";

//데이터 베이스 연결 저장
$conn = mysqli_connect($db_host,$db_user,$db_passwd,$db_name);

if(mysqli_connect_errno($conn)){
  echo "데이터 베이스 연결 실패: " . mysqli_connect_errno();
  echo " <br>";
}else {
  echo "야호! 성공 ";
  echo "<br>";
}
 mysqli_set_charset($conn, 'utf8');
//POST 방식으로 전달된 파라미터 변수 저장
$room_num= $_POST["room_num"];

//update가 끝났으면 학번별로 정렬하여 answ의 값들을 다 더한다
$sql = "CREATE TABLE ".$room_num."_Rank AS";
$sql .= "\n SELECT stu_num, SUM(answ) as score FROM ".$room_num." GROUP BY stu_num";

//rank table 을 만듬
$result = mysqli_query($conn,$sql);
$sql = "SELECT* FROM ".$room_num."_Rank ORDER BY score desc";
$result = mysqli_query($conn,$sql);

//JSON 생성을 위한 배열
$rows = array();
$return = array();

//QUery의 결곽밧을 처움부터 마지막까지 진행하면서 추출
while ($row = mysqli_fetch_array($result)) {
  $rows["stu_num"] = $row["stu_num"]; //학번 추출
  $rows['score'] =(int) $row['score']; // 합산점수


//JSON 데이터 생성을 위한 배열에 레코드 값 추가
  array_push($return, $rows);
  }

  header("Content-type:application/json");
  //json파일 생성
  echo json_encode($return);
  //DB접속 종료
  mysqli_close($conn);

 ?>

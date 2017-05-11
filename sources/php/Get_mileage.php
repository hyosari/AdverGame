<?php
//데이터베이스 접속
$db_host = "localhost";
$db_user = "root";
$db_passwd="asdf";
$db_name = "STUDENT";

//데이터 베이스 연결 저장
$conn = mysqli_connect($db_host,$db_user,$db_passwd,$db_name);

 mysqli_set_charset($conn, 'utf8');

//POST 방식으로 전달된 파라미터 변수 저장
$student_id= $_POST["student_id"];

//id 를 받아 score 를 얻어옴
$sql = "SELECT * FROM UserInfo WHERE stu_num = '".$student_id."'";
//echo $sql ; echo '<br>';
$result = mysqli_query($conn,$sql);

$rows = array();
while($row = mysqli_fetch_array($result)){
$rows['student_id']= $row['stu_num'];
$rows['mileage']= $row['points'];
$rows['game_count']=$row['game_count'];

//echo $rows['student_id'].'학생의 mileage는 '.$rows['mileage'].' 입니다';
//echo '<br>';
}

header("Content-type:application/json");
//json파일 생성
 echo json_encode($rows);
//DB접속 종료
mysqli_close($conn);

 ?>

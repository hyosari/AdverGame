<?php
//데이터베이스 접속
$db_host = "localhost";
$db_user = "root";
$db_passwd="asdf";
$db_name = "STUDENT";

//데이터 베이스 연결 저장
$conn = mysqli_connect($db_host,$db_user,$db_passwd,$db_name);

if(mysqli_connect_errno($conn)){
  echo "데이터 베이스 연결 실패: " . mysqli_connect_errno();
  echo " <br>";
}else {
  echo "set_mileage 접속 성공 ";
  echo "<br>";
}

//POST 방식으로 전달된 파라미터 변수 저장
/*$rankings= array(array('stu_num'=>1234,
                          'score'=>19
                        ),
                  array('stu_num'=>5678,
                        'score'=> 20
                      )
                    );*/

 mysqli_set_charset($conn, 'utf8');
$room_num = $_POST["room_num"];

$sql = "SELECT* FROM ROOM.".$room_num."_Rank ORDER BY 'score'";
$result = mysqli_query($conn,$sql);

//RANK 저장을 위한 배열
$rows = array();
$rankings = array();

//QUery의 결곽밧을 처움부터 마지막까지 진행하면서 추출
while ($row = mysqli_fetch_array($result)) {
  $rows["stu_num"] = $row["stu_num"]; //학번 추출
  $rows['score'] =(int) $row['score']; // 합산점수


//JSON 데이터 생성을 위한 배열에 레코드 값 추가
  array_push($rankings, $rows);
  }


//array 형태로 json 을 받아서 STUDENT Database 에 USER INFO table에 점속

$add_mileage=10000;
$count = 1;
foreach ($rankings as $ranking) {
  $sql= "UPDATE UserInfo SET points= points+".$add_mileage;
  $sql .= ", game_count= game_count+1";
  $sql .= " WHERE stu_num= '".$ranking["stu_num"]."'";

  echo $sql; echo "<br>";
  $add_mileage=$add_mileage-500;

  $result=mysqli_query($conn,$sql);
  if($result)
  {
    echo $count." 위 하신 ".$ranking["stu_num"]." 학생의 mileage UPDATE 성공";
    echo "<br>";
  }else {
    echo $count." 위 하신 ".$ranking["stu_num"]." 학생의 mileage UPDATE 실패";
    echo "<br>";
  }
  $count = $count+1;
}

 ?>

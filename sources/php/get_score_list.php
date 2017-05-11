<?php
//데이터베이스 접속
$connect = mysql_connect("localhost", "root", "asdf");
if($connect == 0){
  echo("no conn\n");
}else{
  echo("conn\n");
}

  //사용할 데이터 베이스 설정
  mysql_select_db("daniel",$connect);

  //POST방식으로 전달된 파라미터를 변수에 저장
  $seq_no = $_POST["seq_no"];

  //sql Query 생성
  $sql = "SELECT";
  $sql .= "\n @rownum:=@rownum+1 as ranking";
  $sql .= "\n     , A.user_name as user_name";
  $sql .= "\n     , A.kill_count as kill_count";
  $sql .= "\n FROM tb_score as A, (SELECT @rownum := 0) R ";
  $sql .= "\n WHERE A.seq_no ='".$seq_no."'";
  $sql .= "\n ORDER BY kill_count DESC";

  //SQL Query 실행
  $result = mysql_query($sql,$connect);

  //JSON 생성을 위한 배열
  $rows = array();
  $return = array();



  //QUery의 결곽밧을 처움부터 마지막 레코드까지 진행하면서 추출
  while ($row = mysql_fetch_array($result)) {
    $rows["ranking"] = $row["ranking"]; //랭킹추출
    $rows["user_name"] = $row["user_name"];
    $rows["kill_count"] = $row["kill_count"];

    //JSON 데이터 생성을 위한 배열에 레코드 값 추가
    array_push($return, $rows);
    }

    header("Content-type:application/json");
    //json파일 생성
    echo json_encode($return);
    //DB접속 종료
    mysql_close($connect);


 ?>

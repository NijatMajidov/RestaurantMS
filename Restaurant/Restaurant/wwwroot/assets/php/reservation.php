<?php require("PHPMailer/PHPMailerAutoload.php");

// ADD your Email and Name
$recipientEmail='wpgoocom@gmail.com';
$recipientName='John Doe';

//collect the posted variables into local variables before calling $mail = new mailer
$senderpeople = $_POST['res-pepole'];
$senderday = $_POST['res-day'];
$sendertime = $_POST['res-time'];
$senderName = $_POST['res-name'];
$senderEmail = $_POST['res-name'];
$senderPhone = $_POST['res-phone'];
$senderRquest = $_POST['res-message'];
$senderSubject = 'New Reservation by ' . $senderName . ' email '.$senderEmail;
//Create a new PHPMailer instance
$mail = new PHPMailer();

//Set who the message is to be sent from
$mail->setFrom($recipientEmail, $recipientName);
//Set an alternative reply-to address
$mail->addReplyTo($recipientEmail,$recipientName);
//Set who the message is to be sent to
$mail->addAddress($recipientEmail, $senderName );
//Set the subject line
$mail->Subject = $senderSubject;

//$mail-&gt;AltBody = "To view the message, please use an HTML compatible email viewer!"; // optional, comment out and test

$mail->MsgHTML($body);
$mail->AddAddress($recipientEmail, $recipientName);

//$mail-&gt;AddAttachment("images/phpmailer.gif"); // attachment
//$mail-&gt;AddAttachment("images/phpmailer_mini.gif"); // attachment

//now make those variables the body of the emails

$mail->Body="
Name: $senderName<br/>
Email: $senderEmail<br/>
Phone: $senderPhone<br/>
Number: $senderpeople <br/>
Day: $senderday<br/>
Time: $sendertime<br/>
Request: $senderRquest<br/>";

if(!$mail->Send()) {
	echo '<div class="alert alert-danger" role="alert">Error: '. $mail->ErrorInfo.'</div>';
} else {
	echo '<div class="alert alert-success" role="alert">Thank you. We will contact you shortly.</div>';
}
?>
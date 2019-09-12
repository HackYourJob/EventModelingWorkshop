name := """sbt"""
organization := "compile"

version := "1.0-SNAPSHOT"

lazy val root = (project in file(".")).enablePlugins(PlayScala, JibPlugin)

scalaVersion := "2.13.0"

libraryDependencies += guice
libraryDependencies += "org.scalatestplus.play" %% "scalatestplus-play" % "4.0.3" % Test

//credentials += Credentials(Path.userHome / ".ivy2" / ".credentials")
//jibTargetImageCredentialHelper := Some("abc")
//jibBaseImage := "docker.io/eventmodeling/payment:1.0-SNAPSHOT"

jibBaseImage    := "openjdk:11-jre"

jibRegistry := "docker.io"
jibOrganization := "eventmodeling"
jibName         := "payment"
jibVersion      := "1.0-SNAPSHOT"
   
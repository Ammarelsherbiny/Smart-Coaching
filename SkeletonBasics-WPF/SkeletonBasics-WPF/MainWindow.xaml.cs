//------------------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Microsoft.Samples.Kinect.SkeletonBasics
{
    using System;
    using System.IO;
    using System.Windows;
    using System.Windows.Media;
    using Microsoft.Kinect;
    using System.Data.SqlClient;
    using System.Data.Sql;
    using MySql.Data.Common;
    using MySql.Data.MySqlClient;
    using System.Collections.Generic;

    
    public partial class MainWindow : Window
    {
       
        private const float RenderWidth = 640.0f;
       
        private const float RenderHeight = 480.0f;

        private const double JointThickness = 3;
       
        private const double BodyCenterThickness = 10;
       
        private const double ClipBoundsThickness = 10;
       
        private readonly Brush centerPointBrush = Brushes.Blue;
       
        private readonly Brush trackedJointBrush = new SolidColorBrush(Color.FromArgb(255, 68, 192, 68));
   
        private readonly Brush inferredJointBrush = Brushes.Yellow;
    
        private readonly Pen trackedBonePen = new Pen(Brushes.Green, 6);
           
        private readonly Pen inferredBonePen = new Pen(Brushes.Gray, 1);
      
        private KinectSensor sensor;
      
        private DrawingGroup drawingGroup;

        private DrawingImage imageSource;
        static string file = "B:\\MIU\\Graduation project\\Trying\\OnlineSystem\\writing\\try2\\data\\video";
        static string txt =".txt";
        static int filenum = 10;
        static string fileW = file + Convert.ToString(filenum)+ txt;
        //StreamWriter sw = new StreamWriter("B:\\MIU\\Graduation project\\Trying\\OnlineSystem\\writing\\try2\\s72.txt");
         StreamWriter sw = new StreamWriter(fileW);

        List<double> Mylist = new List<double>();
        int codenum = 0;
      

        int y = 0;

        public MainWindow()
        {

            InitializeComponent();
           

        }

     
        private static void RenderClippedEdges(Skeleton skeleton, DrawingContext drawingContext)
        {
            if (skeleton.ClippedEdges.HasFlag(FrameEdges.Bottom))
            {
                drawingContext.DrawRectangle(
                    Brushes.Red,
                    null,
                    new Rect(0, RenderHeight - ClipBoundsThickness, RenderWidth, ClipBoundsThickness));
            }

            if (skeleton.ClippedEdges.HasFlag(FrameEdges.Top))
            {
                drawingContext.DrawRectangle(
                    Brushes.Red,
                    null,
                    new Rect(0, 0, RenderWidth, ClipBoundsThickness));
            }

            if (skeleton.ClippedEdges.HasFlag(FrameEdges.Left))
            {
                drawingContext.DrawRectangle(
                    Brushes.Red,
                    null,
                    new Rect(0, 0, ClipBoundsThickness, RenderHeight));
            }

            if (skeleton.ClippedEdges.HasFlag(FrameEdges.Right))
            {
                drawingContext.DrawRectangle(
                    Brushes.Red,
                    null,
                    new Rect(RenderWidth - ClipBoundsThickness, 0, ClipBoundsThickness, RenderHeight));
            }
        }

       
        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            this.drawingGroup = new DrawingGroup();

            this.imageSource = new DrawingImage(this.drawingGroup);

            Image.Source = this.imageSource;

           
            foreach (var potentialSensor in KinectSensor.KinectSensors)
            {
                if (potentialSensor.Status == KinectStatus.Connected)
                {
                    this.sensor = potentialSensor;
                    break;
                }
            }

            if (null != this.sensor)
            {
                // Turn on the skeleton stream to receive skeleton frames
                this.sensor.SkeletonStream.Enable();

                // Add an event handler to be called whenever there is new color frame data
                this.sensor.SkeletonFrameReady += this.SensorSkeletonFrameReady;

                // Start the sensor!
                try
                {
                    this.sensor.Start();
                }
                catch (IOException)
                {
                    this.sensor = null;
                }
            }

            if (null == this.sensor)
            {
                this.statusBarText.Text = Properties.Resources.NoKinectReady;
            }
        }

      
        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (null != this.sensor)
            {
                this.sensor.Stop();
            }
        }


        private static Skeleton GetPrimarySkeleton(Skeleton[] skeletons)
        {
            Skeleton skeleton = null;
            if (skeletons != null)
            {
                for (int i = 0; i < skeletons.Length; i++)
                {
                    if (skeletons[i].TrackingState == SkeletonTrackingState.Tracked)
                    {
                        if (skeleton == null)
                        {
                            skeleton = skeletons[i];
                        }
                        else
                        {
                            if (skeleton.Position.Z > skeletons[i].Position.Z)
                            {
                                skeleton = skeletons[i];
                            }
           
                        }
                    }
                }
            }
            return skeleton;
        }



        void getPositions (Joint j1, Joint j2, Joint j3, Joint j4, Joint j5, Joint j6)
        {
            
            double x1 = j1.Position.X;
            double y1 = j1.Position.Y;
            double z1 = j1.Position.Z;

            double x2 = j2.Position.X;
            double y2 = j2.Position.Y;
            double z2 = j2.Position.Z;

            double x3 = j3.Position.X;
            double y3 = j3.Position.Y;
            double z3 = j3.Position.Z;

            double x4 = j4.Position.X;
            double y4 = j4.Position.Y;
            double z4 = j4.Position.Z;

            double x5 = j5.Position.X;
            double y5 = j5.Position.Y;
            double z5 = j5.Position.Z;
            
            double x6 = j6.Position.X;
            double y6 = j6.Position.Y;
            double z6 = j6.Position.Z;
            
            try
            {
              
                //StreamWriter sw = new StreamWriter("E:\\CSC\\Graduation Project\\Code\\trail1\\trail2.txt");
                //int i;
                string input;
                    input = Console.ReadLine();
                    sw.Write(x1 + ", " + y1 + ", " + z1 + ", ");
                    sw.Write(x2 + ", " + y2 + ", " + z2 + ", ");
                    sw.Write(x3 + ", " + y3 + ", " + z3 + ", ");
                    sw.Write(x4 + ", " + y4 + ", " + z4 + ", ");
                    sw.Write(x5 + ", " + y5 + ", " + z5 + ", ");
                    sw.Write(x6 + ", " + y6 + ", " + z6 + ", ");
                    sw.Write("\n");
                    
                    //label2.Content = xp;
                    //BC1();
                    

                
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }

        }


        void getPositions2 (Joint j1, Joint j2, Joint j3, Joint j4, Joint j5, Joint j6)
        {
            
            double x1 = j1.Position.X;
            double y1 = j1.Position.Y;
            double z1 = j1.Position.Z;

            double x2 = j2.Position.X;
            double y2 = j2.Position.Y;
            double z2 = j2.Position.Z;

            double x3 = j3.Position.X;
            double y3 = j3.Position.Y;
            double z3 = j3.Position.Z;

            double x4 = j4.Position.X;
            double y4 = j4.Position.Y;
            double z4 = j4.Position.Z;

            double x5 = j5.Position.X;
            double y5 = j5.Position.Y;
            double z5 = j5.Position.Z;
            
            double x6 = j6.Position.X;
            double y6 = j6.Position.Y;
            double z6 = j6.Position.Z;
            
            try
            {
              
                
                    
                    //label2.Content = xp;
                    //BC1();
                    string MyConnection2 = "datasource=localhost;port=3306;username=root;password=;database=gptry4;";
                    MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);
                    int id = 10;
                    string cd2 = "INSERT INTO `videopoint`( `VideoId`, `JointId`, `x`, `y`, `z`) VALUES ('" + id + "','1','" + x1 + "','" + y1 + "','" + z1 + "');INSERT INTO `videopoint`( `VideoId`, `JointId`, `x`, `y`, `z`) VALUES ('" + id + "','2','" + x2 + "','" + y2 + "','" + z2 + "');INSERT INTO `videopoint`( `VideoId`, `JointId`, `x`, `y`, `z`) VALUES ('" + id + "','3','" + x3 + "','" + y3 + "','" + z3 + "');INSERT INTO `videopoint`( `VideoId`, `JointId`, `x`, `y`, `z`) VALUES ('" + id + "','4','" + x4 + "','" + y4 + "','" + z4 + "');INSERT INTO `videopoint`( `VideoId`, `JointId`, `x`, `y`, `z`) VALUES ('" + id + "','5','" + x5 + "','" + y5 + "','" + z5 + "');INSERT INTO `videopoint`( `VideoId`, `JointId`, `x`, `y`, `z`) VALUES ('" + id + "','6','" + x6 + "','" + y6 + "','" + z6 + "')";
                    MySqlCommand MyCommand1 = new MySqlCommand(cd2, MyConn2);
                    MyConn2.Open();
                    MySqlDataReader MyReader2;
                    MyReader2 = MyCommand1.ExecuteReader();     
                    //MessageBox.Show("Save Data");
                    while (MyReader2.Read())
                    {
                    }
                    MyConn2.Close();

                
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }

        }

        void getPositions3 (Joint j1, Joint j2, Joint j3, Joint j4, Joint j5, Joint j6)
        {
            
            double x1 = j1.Position.X;
            Mylist.Add(x1);
            double y1 = j1.Position.Y;
            Mylist.Add(y1);
            double z1 = j1.Position.Z;
            Mylist.Add(z1);

            double x2 = j2.Position.X;
            Mylist.Add(x2);
            double y2 = j2.Position.Y;
            Mylist.Add(y2);
            double z2 = j2.Position.Z;
            Mylist.Add(z2);

            double x3 = j3.Position.X;
            Mylist.Add(x3);
            double y3 = j3.Position.Y;
            Mylist.Add(y3);
            double z3 = j3.Position.Z;
            Mylist.Add(z3);

            double x4 = j4.Position.X;
            Mylist.Add(x4);
            double y4 = j4.Position.Y;
            Mylist.Add(y4);
            double z4 = j4.Position.Z;
            Mylist.Add(z4);

            double x5 = j5.Position.X;
            Mylist.Add(x5);
            double y5 = j5.Position.Y;
            Mylist.Add(y5);
            double z5 = j5.Position.Z;
            Mylist.Add(z5);

            double x6 = j6.Position.X;
            Mylist.Add(x6);
            double y6 = j6.Position.Y;
            Mylist.Add(y6);
            double z6 = j6.Position.Z;
            Mylist.Add(z6);
            try
            {
              
                //StreamWriter sw = new StreamWriter("E:\\CSC\\Graduation Project\\Code\\trail1\\trail2.txt");
                //int i;
                string input;
                    input = Console.ReadLine();
                    sw.Write(x1 + ", " + y1 + ", " + z1 + ", ");
                    sw.Write(x2 + ", " + y2 + ", " + z2 + ", ");
                    sw.Write(x3 + ", " + y3 + ", " + z3 + ", ");
                    sw.Write(x4 + ", " + y4 + ", " + z4 + ", ");
                    sw.Write(x5 + ", " + y5 + ", " + z5 + ", ");
                    sw.Write(x6 + ", " + y6 + ", " + z6 + ", ");
                    sw.Write("\n");
                    
                    //label2.Content = xp;
                    //BC1();
                    

                
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            
            

        }



        private void SensorSkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            
            Skeleton[] skeletons = new Skeleton[0];
           
            using (SkeletonFrame skeletonFrame = e.OpenSkeletonFrame())
            {
                if (skeletonFrame != null)
                {
                    skeletons = new Skeleton[skeletonFrame.SkeletonArrayLength];
                    skeletonFrame.CopySkeletonDataTo(skeletons);
                    Skeleton skeleton = GetPrimarySkeleton(skeletons);
                    if (skeleton != null)
                    {
                        Joint WristLeft = skeleton.Joints[JointType.KneeLeft];
                        Joint WristRight = skeleton.Joints[JointType.KneeRight];
                        Joint ShoulderLeft = skeleton.Joints[JointType.HipRight];
                        Joint ShoulderRight = skeleton.Joints[JointType.HipLeft];
                        Joint CenterShoulder = skeleton.Joints[JointType.ShoulderCenter];
                        Joint Spine = skeleton.Joints[JointType.Spine];
                        if(y == 1)
                         { 
                            //getPositions(WristLeft, WristRight, ShoulderLeft, ShoulderRight, CenterShoulder, Spine);
                            getPositions3(WristLeft, WristRight, ShoulderLeft, ShoulderRight, CenterShoulder, Spine);
                            
                        }
                    }
                }
            }
        
            using (DrawingContext dc = this.drawingGroup.Open())
            {
                // Draw a transparent background to set the render size
                dc.DrawRectangle(Brushes.Black, null, new Rect(0.0, 0.0, RenderWidth, RenderHeight));

                if (skeletons.Length != 0)
                {
                    foreach (Skeleton skel in skeletons)
                    {
                        RenderClippedEdges(skel, dc);

                        if (skel.TrackingState == SkeletonTrackingState.Tracked)
                        {
                            this.DrawBonesAndJoints(skel, dc);
                        }
                        else if (skel.TrackingState == SkeletonTrackingState.PositionOnly)
                        {
                            dc.DrawEllipse(
                            this.centerPointBrush,
                            null,
                            this.SkeletonPointToScreen(skel.Position),
                            BodyCenterThickness,
                            BodyCenterThickness);
                        }
                    }
                }

                // prevent drawing outside of our render area
                this.drawingGroup.ClipGeometry = new RectangleGeometry(new Rect(0.0, 0.0, RenderWidth, RenderHeight));
            }
        }


       
        private void DrawBonesAndJoints(Skeleton skeleton, DrawingContext drawingContext)
        {
            // Render Torso
            this.DrawBone(skeleton, drawingContext, JointType.Head, JointType.ShoulderCenter);
            this.DrawBone(skeleton, drawingContext, JointType.ShoulderCenter, JointType.ShoulderLeft);
            this.DrawBone(skeleton, drawingContext, JointType.ShoulderCenter, JointType.ShoulderRight);
            this.DrawBone(skeleton, drawingContext, JointType.ShoulderCenter, JointType.Spine);
            this.DrawBone(skeleton, drawingContext, JointType.Spine, JointType.HipCenter);
            this.DrawBone(skeleton, drawingContext, JointType.HipCenter, JointType.HipLeft);
            this.DrawBone(skeleton, drawingContext, JointType.HipCenter, JointType.HipRight);

            // Left Arm
            this.DrawBone(skeleton, drawingContext, JointType.ShoulderLeft, JointType.ElbowLeft);
            this.DrawBone(skeleton, drawingContext, JointType.ElbowLeft, JointType.WristLeft);
            this.DrawBone(skeleton, drawingContext, JointType.WristLeft, JointType.HandLeft);

            // Right Arm
            this.DrawBone(skeleton, drawingContext, JointType.ShoulderRight, JointType.ElbowRight);
            this.DrawBone(skeleton, drawingContext, JointType.ElbowRight, JointType.WristRight);
            this.DrawBone(skeleton, drawingContext, JointType.WristRight, JointType.HandRight);

            // Left Leg
            this.DrawBone(skeleton, drawingContext, JointType.HipLeft, JointType.KneeLeft);
            this.DrawBone(skeleton, drawingContext, JointType.KneeLeft, JointType.AnkleLeft);
            this.DrawBone(skeleton, drawingContext, JointType.AnkleLeft, JointType.FootLeft);

            // Right Leg
            this.DrawBone(skeleton, drawingContext, JointType.HipRight, JointType.KneeRight);
            this.DrawBone(skeleton, drawingContext, JointType.KneeRight, JointType.AnkleRight);
            this.DrawBone(skeleton, drawingContext, JointType.AnkleRight, JointType.FootRight);
 
            // Render Joints
            foreach (Joint joint in skeleton.Joints)
            {
                Brush drawBrush = null;

                if (joint.TrackingState == JointTrackingState.Tracked)
                {
                    drawBrush = this.trackedJointBrush;                    
                }
                else if (joint.TrackingState == JointTrackingState.Inferred)
                {
                    drawBrush = this.inferredJointBrush;                    
                }

                if (drawBrush != null)
                {
                    drawingContext.DrawEllipse(drawBrush, null, this.SkeletonPointToScreen(joint.Position), JointThickness, JointThickness);
                }
            }
        }

       
        private Point SkeletonPointToScreen(SkeletonPoint skelpoint)
        {
            
            DepthImagePoint depthPoint = this.sensor.CoordinateMapper.MapSkeletonPointToDepthPoint(skelpoint, DepthImageFormat.Resolution640x480Fps30);
            return new Point(depthPoint.X, depthPoint.Y);
        }

        private void DrawBone(Skeleton skeleton, DrawingContext drawingContext, JointType jointType0, JointType jointType1)
        {
            Joint joint0 = skeleton.Joints[jointType0];
            Joint joint1 = skeleton.Joints[jointType1];

            if (joint0.TrackingState == JointTrackingState.NotTracked ||
                joint1.TrackingState == JointTrackingState.NotTracked)
            {
                return;
            }

            if (joint0.TrackingState == JointTrackingState.Inferred &&
                joint1.TrackingState == JointTrackingState.Inferred)
            {
                return;
            }

            Pen drawPen = this.inferredBonePen;
            if (joint0.TrackingState == JointTrackingState.Tracked && joint1.TrackingState == JointTrackingState.Tracked)
            {
                drawPen = this.trackedBonePen;
            }

            drawingContext.DrawLine(drawPen, this.SkeletonPointToScreen(joint0.Position), this.SkeletonPointToScreen(joint1.Position));
        }

        private void CheckBoxSeatedModeChanged(object sender, RoutedEventArgs e)
        {
            if (null != this.sensor)
            {
                if (this.checkBoxSeatedMode.IsChecked.GetValueOrDefault())
                {
                    this.sensor.SkeletonStream.TrackingMode = SkeletonTrackingMode.Seated;
                }
                else
                {
                    this.sensor.SkeletonStream.TrackingMode = SkeletonTrackingMode.Default;
                }
            }
        }

        

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            //StreamWriter sw = new StreamWriter(fileW);

           y =1;

            //sw.Close();
            
        }

        private void CheckBox_UnChecked(object sender, RoutedEventArgs e)
        {
           y =0;
            


            //sw.Close();
            
        }
    
        private void BC()
        {


            

            string MyConnection2 = "datasource=localhost;port=3306;username=root;password=;database=test;";

            string Query = "INSERT INTO `user`(`Id`, `Name`, `Age`) VALUES('', 'ahmed', '19')";

            MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);
            
            MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
            MySqlDataReader MyReader2;
            MyConn2.Open();
            MyReader2 = MyCommand2.ExecuteReader();     
            MessageBox.Show("Save Data");
            while (MyReader2.Read())
            {
            }
            MyConn2.Close();
        }

        private void BCl(object sender, RoutedEventArgs e)
        {
            int count =  Mylist.Count/3;
            int counter=0; 
            string MyConnection2 = "datasource=localhost;port=3306;username=root;password=;database=gpcenter;";
            double x=0,y=0,z=0;
            MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);
            int User_id = 1;
            int movement_id = 1;
            
            string code ="GP1.3gam3a_pre";
            codenum++;
            string co = code + Convert.ToString(codenum);
            string q = "INSERT INTO `video` (`Id`, `Code`, `GroupId`, `MovementId`) VALUES (NULL, '" + co + "','" + User_id + "','" + movement_id + "');";
            MySqlCommand MyCommand1 = new MySqlCommand(q, MyConn2);
            MySqlDataReader MyReader1;
           

            MyConn2.Open();
            //int id = (int)MyCommand1.ExecuteScalar();
            MyReader1 = MyCommand1.ExecuteReader();    
            while (MyReader1.Read())
            {
                //string rid = MyReader1["Id"].ToString();
            }
                        

             MyConn2.Close();
             MyConn2.Open();
            
            string q2 = "SELECT Id FROM `video` WHERE Id=LAST_INSERT_ID()";
             MySqlCommand MyCommand12 = new MySqlCommand(q2, MyConn2);
            MySqlDataReader MyReader12;
           
            MyReader12 = MyCommand12.ExecuteReader();    
            while (MyReader12.Read())
            {
                
            }
            //int id =  rid;
            int rid = (int)MyReader12["Id"];
                int id =  rid;
            MyConn2.Close();

                        
            int jointid= 1;
            for(int j = 0; j < count;j++)
            {
                if(counter <= Mylist.Count  )
                {
                    for(int i= 0; i < 3;i++)
                    {
                        if(i == 0 )
                        {
                            x = Mylist[counter];
                            counter++;
                        
                        }
                        if(i == 1 )
                        {
                            y = Mylist[counter];
                            counter++;
                        
                        }
                        if(i == 2 )
                        {
                            z = Mylist[counter];
                            counter++;
                        
                        }

                    }
                }
                
                string Query = "INSERT INTO `videopoint`( `VideoId`, `JointId`, `x`, `y`, `z`) VALUES ('" + id + "','" + jointid + "','" + x + "','" + y + "','" + z + "');";

                
            
                MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                MySqlDataReader MyReader2;
                MyConn2.Open();
                MyReader2 = MyCommand2.ExecuteReader();     
                //MessageBox.Show("Save Data");
                while (MyReader2.Read())
                {
                }
                MyConn2.Close();
                jointid++;
                if(jointid == 7)
                {
                    jointid = 1;
                }
            }
            Mylist.Clear();
            string qtest = "INSERT INTO `checkvideo` (`Id`, `VideoId`, `Value`, `CoachCheck`) VALUES (NULL, '" + id + "', NULL , '0');";
            MySqlCommand MyCommand3test = new MySqlCommand(qtest, MyConn2);
            MySqlDataReader MyReader3;
           

            MyConn2.Open();
            //int id = (int)MyCommand1.ExecuteScalar();
            MyReader3 = MyCommand3test.ExecuteReader();    
            while (MyReader3.Read())
            {
                //string rid = MyReader1["Id"].ToString();
            }
                        

             MyConn2.Close();
            //sw.Close();
            //filenum++;

        }
    }
}
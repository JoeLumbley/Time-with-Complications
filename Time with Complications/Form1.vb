' Time🕒 with Complications

' MIT License
' Copyright(c) 2024 Joseph W. Lumbley

' Permission Is hereby granted, free Of charge, to any person obtaining a copy
' of this software And associated documentation files (the "Software"), to deal
' in the Software without restriction, including without limitation the rights
' to use, copy, modify, merge, publish, distribute, sublicense, And/Or sell
' copies of the Software, And to permit persons to whom the Software Is
' furnished to do so, subject to the following conditions:

' The above copyright notice And this permission notice shall be included In all
' copies Or substantial portions of the Software.

' THE SOFTWARE Is PROVIDED "AS IS", WITHOUT WARRANTY Of ANY KIND, EXPRESS Or
' IMPLIED, INCLUDING BUT Not LIMITED To THE WARRANTIES Of MERCHANTABILITY,
' FITNESS FOR A PARTICULAR PURPOSE And NONINFRINGEMENT. IN NO EVENT SHALL THE
' AUTHORS Or COPYRIGHT HOLDERS BE LIABLE For ANY CLAIM, DAMAGES Or OTHER
' LIABILITY, WHETHER In AN ACTION Of CONTRACT, TORT Or OTHERWISE, ARISING FROM,
' OUT OF Or IN CONNECTION WITH THE SOFTWARE Or THE USE Or OTHER DEALINGS IN THE
' SOFTWARE.

' Monica is our an AI assistant.
' https://monica.im/

Public Class Form1

    Private Context As BufferedGraphicsContext
    Private Buffer As BufferedGraphics
    Private TimeZone As String


    Private TimeZoneCity As String

    Private Enum HourFormat
        Twelve
        TwentyFour
    End Enum

    Private Enum InfoType
        Time
        LongDayOfWeek
        ShortDayOfWeek
        LongDate
        MedDate
        ShortDate
        TimeZone
        TimeZoneCity
    End Enum

    Private Hours As HourFormat = HourFormat.Twelve

    Private Structure DisplayObject
        Public location As Point
        Public text As String
        Public font As Font
        Public info As InfoType

    End Structure

    Private MainDisplay As DisplayObject
    Private TopDisplay As DisplayObject
    Private BottomDisplay As DisplayObject

    Private ReadOnly AlineCenterMiddle As New StringFormat With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center}

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        InitializeForm()

        InitializeBuffer()

        TopDisplay.info = InfoType.LongDayOfWeek

        BottomDisplay.info = InfoType.MedDate

        Timer1.Interval = 20

        Timer1.Enabled = True

        Debug.Print($"Program running... {Now.ToShortTimeString}")

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        If Hours = HourFormat.Twelve Then

            ' Formats the current time to 12-hour (Regular Time)
            MainDisplay.text = Now.ToLocalTime.ToShortTimeString()

        Else

            ' Formats the current time to 24-hour (Military Time)
            MainDisplay.text = Now.ToLocalTime.ToString("HH:mm")

        End If

        Select Case TopDisplay.info

            Case InfoType.LongDayOfWeek

                TopDisplay.text = Now.DayOfWeek.ToString

            Case InfoType.ShortDayOfWeek

                Select Case Now.DayOfWeek
                    Case DayOfWeek.Sunday
                        TopDisplay.text = "Sun"
                    Case DayOfWeek.Monday
                        TopDisplay.text = "Mon"
                    Case DayOfWeek.Tuesday
                        TopDisplay.text = "Tue"
                    Case DayOfWeek.Wednesday
                        TopDisplay.text = "Wed"
                    Case DayOfWeek.Thursday
                        TopDisplay.text = "Thu"
                    Case DayOfWeek.Friday
                        TopDisplay.text = "Fri"
                    Case DayOfWeek.Saturday
                        TopDisplay.text = "Sat"
                End Select

            Case InfoType.LongDate

                TopDisplay.text = Now.ToLongDateString

            Case InfoType.MedDate

                TopDisplay.text = Now.ToString("MMMM d, yyyy")

            Case InfoType.ShortDate

                TopDisplay.text = Now.ToShortDateString

            Case InfoType.TimeZone

                TopDisplay.text = TimeZoneInfo.Local.Id

            Case InfoType.TimeZoneCity

                Select Case TimeZoneInfo.Local.Id
                    Case "Eastern Standard Time"
                        TimeZoneCity = "New York"
                    Case "Central Standard Time"
                        TimeZoneCity = "Chicago"
                    Case "Mountain Standard Time"
                        TimeZoneCity = "Denver"
                    Case "Pacific Standard Time"
                        TimeZoneCity = "Los Angeles"
                    Case Else
                        TimeZoneCity = "Unknown"
                End Select

                TopDisplay.text = TimeZoneCity

        End Select





        'Dim timeZone As TimeZoneInfo = TimeZoneInfo.Local

        'If timeZone = TimeZoneInfo.Id Then


        'End If





        'TopDisplay.text = Now.DayOfWeek.ToString
        'BottomDisplay.text = Now.Date()
        'BottomDisplay.text = Now.ToString("MMMM d, yyyy")

        BottomDisplay.text = Now.DayOfWeek.ToString

        'TopDisplay.text = Now.ToLocalTime.ToShortTimeString() & Environment.NewLine & TimeZoneCity ' Formats the current time to 12-hour format and time zone


        'DisplayText = Now.ToLocalTime.ToString("HH:mm") ' Formats the current time to 24-hour format (military time)



        If Not WindowState = FormWindowState.Minimized Then

            Refresh() ' Calls OnPaint Sub


        End If


    End Sub

    Private Sub Form1_Resize(sender As Object, e As EventArgs) Handles Me.Resize

        If Not WindowState = FormWindowState.Minimized Then

            ' Set the font size for the main display based on the width of the client rectangle
            Dim FontSize As Integer = ClientSize.Width \ 14
            MainDisplay.font = New Font("Segoe UI", FontSize, FontStyle.Regular)

            ' Center the main display in the client rectangle.
            MainDisplay.location.X = ClientSize.Width \ 2
            MainDisplay.location.Y = (ClientSize.Height + MenuStrip1.Height) \ 2

            ' Set the font size for the top display based on the width of the client rectangle
            FontSize = ClientSize.Width \ 41
            TopDisplay.font = New Font("Segoe UI", FontSize, FontStyle.Regular)

            ' Center the top display in the client rectangle above the main display.
            TopDisplay.location.X = ClientSize.Width \ 2
            TopDisplay.location.Y = (ClientSize.Height + MenuStrip1.Height) \ 2 - ClientSize.Width \ 10

            ' Set the font size for the bottom display based on the width of the client rectangle
            'FontSize = ClientSize.Width \ 39
            BottomDisplay.font = New Font("Segoe UI", FontSize, FontStyle.Regular)

            ' Center the bottom display in the client rectangle below the main display.
            BottomDisplay.location.X = ClientSize.Width \ 2
            BottomDisplay.location.Y = (ClientSize.Height + MenuStrip1.Height) \ 2 + ClientSize.Width \ 10

            ' Dispose of the existing buffer
            If Buffer IsNot Nothing Then

                Buffer.Dispose()

                Buffer = Nothing ' Set to Nothing to avoid using a disposed object

            End If

            ' The buffer will be reallocated in OnPaint

        End If

    End Sub

    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)

        ' Allocate the buffer if it hasn't been allocated yet
        If Buffer Is Nothing Then

            Buffer = Context.Allocate(e.Graphics, ClientRectangle)

        End If

        DrawFrame()

        Buffer.Render(e.Graphics)

    End Sub

    Private Sub DrawFrame()

        If Buffer IsNot Nothing Then

            Try

                With Buffer.Graphics

                    .Clear(Color.Black)

                    .CompositingMode = Drawing2D.CompositingMode.SourceOver
                    .TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias
                    .SmoothingMode = Drawing2D.SmoothingMode.HighQuality
                    .PixelOffsetMode = Drawing2D.PixelOffsetMode.None
                    .CompositingQuality = Drawing2D.CompositingQuality.HighQuality

                    .DrawString(MainDisplay.text, MainDisplay.font, Brushes.White, MainDisplay.location, AlineCenterMiddle)

                    .DrawString(TopDisplay.text, TopDisplay.font, Brushes.LightGray, TopDisplay.location, AlineCenterMiddle)

                    .DrawString(BottomDisplay.text, BottomDisplay.font, Brushes.LightGray, BottomDisplay.location, AlineCenterMiddle)

                End With

            Catch ex As Exception

                Debug.Print("Draw error: " & ex.Message)

            End Try

        Else

            Debug.Print("Buffer is not initialized.")

        End If

    End Sub

    Private Sub InitializeForm()

        CenterToScreen()

        SetStyle(ControlStyles.UserPaint, True)

        SetStyle(ControlStyles.OptimizedDoubleBuffer, True)

        SetStyle(ControlStyles.AllPaintingInWmPaint, True)

        Text = "Time🕒 with Complications - Code with Joe"

        Me.WindowState = FormWindowState.Maximized

    End Sub

    Private Sub InitializeBuffer()

        ' Set context to the context of this app.
        Context = BufferedGraphicsManager.Current

        Context.MaximumBuffer = Screen.PrimaryScreen.WorkingArea.Size

        ' Allocate the buffer initially using the current client rectangle
        Buffer = Context.Allocate(CreateGraphics(), ClientRectangle)

    End Sub

    Private Sub TwentyFourHourMenuItem_Click(sender As Object, e As EventArgs) Handles TwentyFourHourMenuItem.Click

        If Not Hours = HourFormat.TwentyFour Then Hours = HourFormat.TwentyFour

        If Not TwentyFourHourMenuItem.Checked Then TwentyFourHourMenuItem.Checked = True

        ' Uncheck the other menu option
        If TwelveHourMenuItem.Checked Then TwelveHourMenuItem.Checked = False

    End Sub

    Private Sub TwelveHourMenuItem_Click(sender As Object, e As EventArgs) Handles TwelveHourMenuItem.Click

        If Not Hours = HourFormat.Twelve Then Hours = HourFormat.Twelve

        If Not TwelveHourMenuItem.Checked Then TwelveHourMenuItem.Checked = True

        ' Uncheck the other menu option
        If TwentyFourHourMenuItem.Checked Then TwentyFourHourMenuItem.Checked = False

    End Sub

    Private Sub TopDayMenuItem_Click(sender As Object, e As EventArgs) Handles TopDayMenuItem.Click

        If Not TopDisplay.info = InfoType.LongDayOfWeek Then TopDisplay.info = InfoType.LongDayOfWeek

        If Not TopDayMenuItem.Checked Then TopDayMenuItem.Checked = True

        ' Uncheck the other menu options
        If TopShortDayMenuItem.Checked Then TopShortDayMenuItem.Checked = False
        If TopLongDateMenuItem.Checked Then TopLongDateMenuItem.Checked = False
        If TopMedDateMenuItem.Checked Then TopMedDateMenuItem.Checked = False
        If TopShortDateMenuItem.Checked Then TopShortDateMenuItem.Checked = False
        If TopTimeZoneMenuItem.Checked Then TopTimeZoneMenuItem.Checked = False
        If TopTimeZoneCityMenuItem.Checked Then TopTimeZoneCityMenuItem.Checked = False

    End Sub

    Private Sub TopShortDayMenuItem_Click(sender As Object, e As EventArgs) Handles TopShortDayMenuItem.Click

        If Not TopDisplay.info = InfoType.ShortDayOfWeek Then TopDisplay.info = InfoType.ShortDayOfWeek

        If Not TopShortDayMenuItem.Checked Then TopShortDayMenuItem.Checked = True

        ' Uncheck the other menu options
        If TopDayMenuItem.Checked Then TopDayMenuItem.Checked = False
        If TopLongDateMenuItem.Checked Then TopLongDateMenuItem.Checked = False
        If TopMedDateMenuItem.Checked Then TopMedDateMenuItem.Checked = False
        If TopShortDateMenuItem.Checked Then TopShortDateMenuItem.Checked = False
        If TopTimeZoneMenuItem.Checked Then TopTimeZoneMenuItem.Checked = False
        If TopTimeZoneCityMenuItem.Checked Then TopTimeZoneCityMenuItem.Checked = False

    End Sub

    Private Sub TopLongDateMenuItem_Click(sender As Object, e As EventArgs) Handles TopLongDateMenuItem.Click

        If Not TopDisplay.info = InfoType.LongDate Then TopDisplay.info = InfoType.LongDate

        If Not TopLongDateMenuItem.Checked Then TopLongDateMenuItem.Checked = True

        ' Uncheck the other menu options
        If TopDayMenuItem.Checked Then TopDayMenuItem.Checked = False
        If TopShortDayMenuItem.Checked Then TopShortDayMenuItem.Checked = False
        If TopMedDateMenuItem.Checked Then TopMedDateMenuItem.Checked = False
        If TopShortDateMenuItem.Checked Then TopShortDateMenuItem.Checked = False
        If TopTimeZoneMenuItem.Checked Then TopTimeZoneMenuItem.Checked = False
        If TopTimeZoneCityMenuItem.Checked Then TopTimeZoneCityMenuItem.Checked = False

    End Sub

    Private Sub TopMedDateMenuItem_Click(sender As Object, e As EventArgs) Handles TopMedDateMenuItem.Click

        If Not TopDisplay.info = InfoType.MedDate Then TopDisplay.info = InfoType.MedDate

        If Not TopMedDateMenuItem.Checked Then TopMedDateMenuItem.Checked = True

        ' Uncheck the other menu options
        If TopDayMenuItem.Checked Then TopDayMenuItem.Checked = False
        If TopShortDayMenuItem.Checked Then TopShortDayMenuItem.Checked = False
        If TopLongDateMenuItem.Checked Then TopLongDateMenuItem.Checked = False
        If TopShortDateMenuItem.Checked Then TopShortDateMenuItem.Checked = False
        If TopTimeZoneMenuItem.Checked Then TopTimeZoneMenuItem.Checked = False
        If TopTimeZoneCityMenuItem.Checked Then TopTimeZoneCityMenuItem.Checked = False

    End Sub

    Private Sub TopShortDateMenuItem_Click(sender As Object, e As EventArgs) Handles TopShortDateMenuItem.Click

        If Not TopDisplay.info = InfoType.ShortDate Then TopDisplay.info = InfoType.ShortDate

        If Not TopShortDateMenuItem.Checked Then TopShortDateMenuItem.Checked = True

        ' Uncheck the other menu options
        If TopDayMenuItem.Checked Then TopDayMenuItem.Checked = False
        If TopShortDayMenuItem.Checked Then TopShortDayMenuItem.Checked = False
        If TopLongDateMenuItem.Checked Then TopLongDateMenuItem.Checked = False
        If TopMedDateMenuItem.Checked Then TopMedDateMenuItem.Checked = False
        If TopTimeZoneMenuItem.Checked Then TopTimeZoneMenuItem.Checked = False
        If TopTimeZoneCityMenuItem.Checked Then TopTimeZoneCityMenuItem.Checked = False

    End Sub

    Private Sub TopTimeZoneMenuItem_Click(sender As Object, e As EventArgs) Handles TopTimeZoneMenuItem.Click

        If Not TopDisplay.info = InfoType.TimeZone Then TopDisplay.info = InfoType.TimeZone

        If Not TopTimeZoneMenuItem.Checked Then TopTimeZoneMenuItem.Checked = True

        ' Uncheck the other menu options
        If TopDayMenuItem.Checked Then TopDayMenuItem.Checked = False
        If TopShortDayMenuItem.Checked Then TopShortDayMenuItem.Checked = False
        If TopLongDateMenuItem.Checked Then TopLongDateMenuItem.Checked = False
        If TopMedDateMenuItem.Checked Then TopMedDateMenuItem.Checked = False
        If TopShortDateMenuItem.Checked Then TopShortDateMenuItem.Checked = False
        If TopTimeZoneCityMenuItem.Checked Then TopTimeZoneCityMenuItem.Checked = False

    End Sub

    Private Sub TopTimeZoneCityMenuItem_Click(sender As Object, e As EventArgs) Handles TopTimeZoneCityMenuItem.Click

        If Not TopDisplay.info = InfoType.TimeZoneCity Then TopDisplay.info = InfoType.TimeZoneCity

        If Not TopTimeZoneCityMenuItem.Checked Then TopTimeZoneCityMenuItem.Checked = True

        ' Uncheck the other menu options
        If TopDayMenuItem.Checked Then TopDayMenuItem.Checked = False
        If TopShortDayMenuItem.Checked Then TopShortDayMenuItem.Checked = False
        If TopLongDateMenuItem.Checked Then TopLongDateMenuItem.Checked = False
        If TopMedDateMenuItem.Checked Then TopMedDateMenuItem.Checked = False
        If TopShortDateMenuItem.Checked Then TopShortDateMenuItem.Checked = False
        If TopTimeZoneMenuItem.Checked Then TopTimeZoneMenuItem.Checked = False

    End Sub



End Class

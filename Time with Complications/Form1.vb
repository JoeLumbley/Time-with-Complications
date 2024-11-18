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

    Private Enum HourFormat
        Twelve
        TwentyFour
    End Enum

    Private Hours As HourFormat = HourFormat.Twelve

    Private Enum InfoType
        Time
        LongDayOfWeek
        ShortDayOfWeek
        LongDate
        MedDate
        ShortDate
        TimeZone
        TimeZoneCity
        LocalTime
    End Enum

    Private Structure DisplayObject
        Public Location As Point
        Public Text As String
        Public Font As Font
        Public Type As InfoType
    End Structure

    Private MainDisplay As DisplayObject
    Private TopDisplay As DisplayObject
    Private BottomDisplay As DisplayObject

    Private ReadOnly AlineCenterMiddle As New StringFormat With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center}

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        InitializeForm()

        InitializeBuffer()

        TopDisplay.Type = InfoType.LongDayOfWeek

        BottomDisplay.Type = InfoType.MedDate

        Timer1.Interval = 20

        Timer1.Enabled = True

        Debug.Print($"Program running... {Now.ToShortTimeString}")

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        If Hours = HourFormat.Twelve Then

            ' Formats the current time to 12-hour (Regular Time)
            MainDisplay.Text = Now.ToLocalTime.ToShortTimeString()

        Else

            ' Formats the current time to 24-hour (Military Time)
            MainDisplay.Text = Now.ToLocalTime.ToString("HH:mm")

        End If

        Select Case TopDisplay.Type

            Case InfoType.LongDayOfWeek

                TopDisplay.Text = Now.DayOfWeek.ToString

            Case InfoType.ShortDayOfWeek

                Select Case Now.DayOfWeek
                    Case DayOfWeek.Sunday
                        TopDisplay.Text = "Sun"
                    Case DayOfWeek.Monday
                        TopDisplay.Text = "Mon"
                    Case DayOfWeek.Tuesday
                        TopDisplay.Text = "Tue"
                    Case DayOfWeek.Wednesday
                        TopDisplay.Text = "Wed"
                    Case DayOfWeek.Thursday
                        TopDisplay.Text = "Thu"
                    Case DayOfWeek.Friday
                        TopDisplay.Text = "Fri"
                    Case DayOfWeek.Saturday
                        TopDisplay.Text = "Sat"
                End Select

            Case InfoType.LongDate

                TopDisplay.Text = Now.ToLongDateString

            Case InfoType.MedDate

                TopDisplay.Text = Now.ToString("MMMM d, yyyy")

            Case InfoType.ShortDate

                TopDisplay.Text = Now.ToShortDateString

            Case InfoType.TimeZone

                TopDisplay.Text = TimeZoneInfo.Local.Id

            Case InfoType.TimeZoneCity

                Select Case TimeZoneInfo.Local.Id
                    Case "Eastern Standard Time"
                        TopDisplay.Text = "New York"
                    Case "Central Standard Time"
                        TopDisplay.Text = "Chicago"
                    Case "Mountain Standard Time"
                        TopDisplay.Text = "Denver"
                    Case "Pacific Standard Time"
                        TopDisplay.Text = "Los Angeles"
                    Case Else
                        TopDisplay.Text = "Unknown"
                End Select

            Case InfoType.LocalTime

                TopDisplay.Text = "Local Time"

        End Select

        Select Case BottomDisplay.Type

            Case InfoType.LongDayOfWeek

                BottomDisplay.Text = Now.DayOfWeek.ToString

            Case InfoType.ShortDayOfWeek

                Select Case Now.DayOfWeek
                    Case DayOfWeek.Sunday
                        BottomDisplay.Text = "Sun"
                    Case DayOfWeek.Monday
                        BottomDisplay.Text = "Mon"
                    Case DayOfWeek.Tuesday
                        BottomDisplay.Text = "Tue"
                    Case DayOfWeek.Wednesday
                        BottomDisplay.Text = "Wed"
                    Case DayOfWeek.Thursday
                        BottomDisplay.Text = "Thu"
                    Case DayOfWeek.Friday
                        BottomDisplay.Text = "Fri"
                    Case DayOfWeek.Saturday
                        BottomDisplay.Text = "Sat"
                End Select

            Case InfoType.LongDate

                BottomDisplay.Text = Now.ToLongDateString

            Case InfoType.MedDate

                BottomDisplay.Text = Now.ToString("MMMM d, yyyy")

            Case InfoType.ShortDate

                BottomDisplay.Text = Now.ToShortDateString

            Case InfoType.TimeZone

                BottomDisplay.Text = TimeZoneInfo.Local.Id

            Case InfoType.TimeZoneCity

                Select Case TimeZoneInfo.Local.Id
                    Case "Eastern Standard Time"
                        BottomDisplay.Text = "New York"
                    Case "Central Standard Time"
                        BottomDisplay.Text = "Chicago"
                    Case "Mountain Standard Time"
                        BottomDisplay.Text = "Denver"
                    Case "Pacific Standard Time"
                        BottomDisplay.Text = "Los Angeles"
                    Case Else
                        BottomDisplay.Text = "Unknown"
                End Select

            Case InfoType.LocalTime

                BottomDisplay.Text = "Local Time"

        End Select

        If Not WindowState = FormWindowState.Minimized Then

            Refresh() ' Calls OnPaint Sub

        End If

    End Sub

    Private Sub Form1_Resize(sender As Object, e As EventArgs) Handles Me.Resize

        If Not WindowState = FormWindowState.Minimized Then

            ' Set the font size for the main display based on the width of the client rectangle
            Dim FontSize As Integer = ClientSize.Width \ 14
            MainDisplay.Font = New Font("Segoe UI", FontSize, FontStyle.Regular)

            ' Center the main display in the client rectangle.
            MainDisplay.Location.X = ClientSize.Width \ 2
            MainDisplay.Location.Y = (ClientSize.Height + MenuStrip1.Height) \ 2

            ' Set the font size for the top display based on the width of the client rectangle
            FontSize = ClientSize.Width \ 41
            TopDisplay.Font = New Font("Segoe UI", FontSize, FontStyle.Regular)

            ' Center the top display in the client rectangle above the main display.
            TopDisplay.Location.X = ClientSize.Width \ 2
            TopDisplay.Location.Y = (ClientSize.Height + MenuStrip1.Height) \ 2 - ClientSize.Width \ 10

            ' Set the font size for the bottom display based on the width of the client rectangle
            'FontSize = ClientSize.Width \ 39
            BottomDisplay.Font = New Font("Segoe UI", FontSize, FontStyle.Regular)

            ' Center the bottom display in the client rectangle below the main display.
            BottomDisplay.Location.X = ClientSize.Width \ 2
            BottomDisplay.Location.Y = (ClientSize.Height + MenuStrip1.Height) \ 2 + ClientSize.Width \ 10

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

                    .DrawString(MainDisplay.Text, MainDisplay.Font, Brushes.White, MainDisplay.Location, AlineCenterMiddle)

                    .DrawString(TopDisplay.Text, TopDisplay.Font, Brushes.LightGray, TopDisplay.Location, AlineCenterMiddle)

                    .DrawString(BottomDisplay.Text, BottomDisplay.Font, Brushes.LightGray, BottomDisplay.Location, AlineCenterMiddle)

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

        If Not TopDisplay.Type = InfoType.LongDayOfWeek Then TopDisplay.Type = InfoType.LongDayOfWeek

        If Not TopDayMenuItem.Checked Then TopDayMenuItem.Checked = True

        ' Uncheck the other menu options
        If TopShortDayMenuItem.Checked Then TopShortDayMenuItem.Checked = False
        If TopLongDateMenuItem.Checked Then TopLongDateMenuItem.Checked = False
        If TopMedDateMenuItem.Checked Then TopMedDateMenuItem.Checked = False
        If TopShortDateMenuItem.Checked Then TopShortDateMenuItem.Checked = False
        If TopTimeZoneMenuItem.Checked Then TopTimeZoneMenuItem.Checked = False
        If TopTimeZoneCityMenuItem.Checked Then TopTimeZoneCityMenuItem.Checked = False
        If TopLocalTimeMenuItem.Checked Then TopLocalTimeMenuItem.Checked = False

    End Sub

    Private Sub TopShortDayMenuItem_Click(sender As Object, e As EventArgs) Handles TopShortDayMenuItem.Click

        If Not TopDisplay.Type = InfoType.ShortDayOfWeek Then TopDisplay.Type = InfoType.ShortDayOfWeek

        If Not TopShortDayMenuItem.Checked Then TopShortDayMenuItem.Checked = True

        ' Uncheck the other menu options
        If TopDayMenuItem.Checked Then TopDayMenuItem.Checked = False
        If TopLongDateMenuItem.Checked Then TopLongDateMenuItem.Checked = False
        If TopMedDateMenuItem.Checked Then TopMedDateMenuItem.Checked = False
        If TopShortDateMenuItem.Checked Then TopShortDateMenuItem.Checked = False
        If TopTimeZoneMenuItem.Checked Then TopTimeZoneMenuItem.Checked = False
        If TopTimeZoneCityMenuItem.Checked Then TopTimeZoneCityMenuItem.Checked = False
        If TopLocalTimeMenuItem.Checked Then TopLocalTimeMenuItem.Checked = False

    End Sub

    Private Sub TopLongDateMenuItem_Click(sender As Object, e As EventArgs) Handles TopLongDateMenuItem.Click

        If Not TopDisplay.Type = InfoType.LongDate Then TopDisplay.Type = InfoType.LongDate

        If Not TopLongDateMenuItem.Checked Then TopLongDateMenuItem.Checked = True

        ' Uncheck the other menu options
        If TopDayMenuItem.Checked Then TopDayMenuItem.Checked = False
        If TopShortDayMenuItem.Checked Then TopShortDayMenuItem.Checked = False
        If TopMedDateMenuItem.Checked Then TopMedDateMenuItem.Checked = False
        If TopShortDateMenuItem.Checked Then TopShortDateMenuItem.Checked = False
        If TopTimeZoneMenuItem.Checked Then TopTimeZoneMenuItem.Checked = False
        If TopTimeZoneCityMenuItem.Checked Then TopTimeZoneCityMenuItem.Checked = False
        If TopLocalTimeMenuItem.Checked Then TopLocalTimeMenuItem.Checked = False

    End Sub

    Private Sub TopMedDateMenuItem_Click(sender As Object, e As EventArgs) Handles TopMedDateMenuItem.Click

        If Not TopDisplay.Type = InfoType.MedDate Then TopDisplay.Type = InfoType.MedDate

        If Not TopMedDateMenuItem.Checked Then TopMedDateMenuItem.Checked = True

        ' Uncheck the other menu options
        If TopDayMenuItem.Checked Then TopDayMenuItem.Checked = False
        If TopShortDayMenuItem.Checked Then TopShortDayMenuItem.Checked = False
        If TopLongDateMenuItem.Checked Then TopLongDateMenuItem.Checked = False
        If TopShortDateMenuItem.Checked Then TopShortDateMenuItem.Checked = False
        If TopTimeZoneMenuItem.Checked Then TopTimeZoneMenuItem.Checked = False
        If TopTimeZoneCityMenuItem.Checked Then TopTimeZoneCityMenuItem.Checked = False
        If TopLocalTimeMenuItem.Checked Then TopLocalTimeMenuItem.Checked = False

    End Sub

    Private Sub TopShortDateMenuItem_Click(sender As Object, e As EventArgs) Handles TopShortDateMenuItem.Click

        If Not TopDisplay.Type = InfoType.ShortDate Then TopDisplay.Type = InfoType.ShortDate

        If Not TopShortDateMenuItem.Checked Then TopShortDateMenuItem.Checked = True

        ' Uncheck the other menu options
        If TopDayMenuItem.Checked Then TopDayMenuItem.Checked = False
        If TopShortDayMenuItem.Checked Then TopShortDayMenuItem.Checked = False
        If TopLongDateMenuItem.Checked Then TopLongDateMenuItem.Checked = False
        If TopMedDateMenuItem.Checked Then TopMedDateMenuItem.Checked = False
        If TopTimeZoneMenuItem.Checked Then TopTimeZoneMenuItem.Checked = False
        If TopTimeZoneCityMenuItem.Checked Then TopTimeZoneCityMenuItem.Checked = False
        If TopLocalTimeMenuItem.Checked Then TopLocalTimeMenuItem.Checked = False

    End Sub

    Private Sub TopTimeZoneMenuItem_Click(sender As Object, e As EventArgs) Handles TopTimeZoneMenuItem.Click

        If Not TopDisplay.Type = InfoType.TimeZone Then TopDisplay.Type = InfoType.TimeZone

        If Not TopTimeZoneMenuItem.Checked Then TopTimeZoneMenuItem.Checked = True

        ' Uncheck the other menu options
        If TopDayMenuItem.Checked Then TopDayMenuItem.Checked = False
        If TopShortDayMenuItem.Checked Then TopShortDayMenuItem.Checked = False
        If TopLongDateMenuItem.Checked Then TopLongDateMenuItem.Checked = False
        If TopMedDateMenuItem.Checked Then TopMedDateMenuItem.Checked = False
        If TopShortDateMenuItem.Checked Then TopShortDateMenuItem.Checked = False
        If TopTimeZoneCityMenuItem.Checked Then TopTimeZoneCityMenuItem.Checked = False
        If TopLocalTimeMenuItem.Checked Then TopLocalTimeMenuItem.Checked = False

    End Sub

    Private Sub TopTimeZoneCityMenuItem_Click(sender As Object, e As EventArgs) Handles TopTimeZoneCityMenuItem.Click

        If Not TopDisplay.Type = InfoType.TimeZoneCity Then TopDisplay.Type = InfoType.TimeZoneCity

        If Not TopTimeZoneCityMenuItem.Checked Then TopTimeZoneCityMenuItem.Checked = True

        ' Uncheck the other menu options
        If TopDayMenuItem.Checked Then TopDayMenuItem.Checked = False
        If TopShortDayMenuItem.Checked Then TopShortDayMenuItem.Checked = False
        If TopLongDateMenuItem.Checked Then TopLongDateMenuItem.Checked = False
        If TopMedDateMenuItem.Checked Then TopMedDateMenuItem.Checked = False
        If TopShortDateMenuItem.Checked Then TopShortDateMenuItem.Checked = False
        If TopTimeZoneMenuItem.Checked Then TopTimeZoneMenuItem.Checked = False
        If TopLocalTimeMenuItem.Checked Then TopLocalTimeMenuItem.Checked = False

    End Sub

    Private Sub TopLocalTimeMenuItem_Click(sender As Object, e As EventArgs) Handles TopLocalTimeMenuItem.Click

        If Not TopDisplay.Type = InfoType.LocalTime Then TopDisplay.Type = InfoType.LocalTime

        If Not TopLocalTimeMenuItem.Checked Then TopLocalTimeMenuItem.Checked = True

        ' Uncheck the other menu options
        If TopDayMenuItem.Checked Then TopDayMenuItem.Checked = False
        If TopShortDayMenuItem.Checked Then TopShortDayMenuItem.Checked = False
        If TopLongDateMenuItem.Checked Then TopLongDateMenuItem.Checked = False
        If TopMedDateMenuItem.Checked Then TopMedDateMenuItem.Checked = False
        If TopShortDateMenuItem.Checked Then TopShortDateMenuItem.Checked = False
        If TopTimeZoneMenuItem.Checked Then TopTimeZoneMenuItem.Checked = False
        If TopTimeZoneCityMenuItem.Checked Then TopTimeZoneCityMenuItem.Checked = False

    End Sub

    Private Sub BottomDayMenuItem_Click(sender As Object, e As EventArgs) Handles BottomDayMenuItem.Click

        If Not BottomDisplay.Type = InfoType.LongDayOfWeek Then BottomDisplay.Type = InfoType.LongDayOfWeek

        If Not BottomDayMenuItem.Checked Then BottomDayMenuItem.Checked = True

        ' Uncheck the other menu options
        If BottomShortDayMenuItem.Checked Then BottomShortDayMenuItem.Checked = False
        If BottomLongDateMenuItem.Checked Then BottomLongDateMenuItem.Checked = False
        If BottomMediumDateMenuItem.Checked Then BottomMediumDateMenuItem.Checked = False
        If BottomShortDateMenuItem.Checked Then BottomShortDateMenuItem.Checked = False
        If BottomTimeZoneMenuItem.Checked Then BottomTimeZoneMenuItem.Checked = False
        If BottomTimeZoneCityMenuItem.Checked Then BottomTimeZoneCityMenuItem.Checked = False
        If BottomLocalTimeMenuItem.Checked Then BottomLocalTimeMenuItem.Checked = False

    End Sub

    Private Sub BottomShortDayMenuItem_Click(sender As Object, e As EventArgs) Handles BottomShortDayMenuItem.Click

        If Not BottomDisplay.Type = InfoType.ShortDayOfWeek Then BottomDisplay.Type = InfoType.ShortDayOfWeek

        If Not BottomShortDayMenuItem.Checked Then BottomShortDayMenuItem.Checked = True

        ' Uncheck the other menu options
        If BottomDayMenuItem.Checked Then BottomDayMenuItem.Checked = False
        If BottomLongDateMenuItem.Checked Then BottomLongDateMenuItem.Checked = False
        If BottomMediumDateMenuItem.Checked Then BottomMediumDateMenuItem.Checked = False
        If BottomShortDateMenuItem.Checked Then BottomShortDateMenuItem.Checked = False
        If BottomTimeZoneMenuItem.Checked Then BottomTimeZoneMenuItem.Checked = False
        If BottomTimeZoneCityMenuItem.Checked Then BottomTimeZoneCityMenuItem.Checked = False
        If BottomLocalTimeMenuItem.Checked Then BottomLocalTimeMenuItem.Checked = False

    End Sub

    Private Sub BottomLongDateMenuItem_Click(sender As Object, e As EventArgs) Handles BottomLongDateMenuItem.Click

        If Not BottomDisplay.Type = InfoType.LongDate Then BottomDisplay.Type = InfoType.LongDate

        If Not BottomLongDateMenuItem.Checked Then BottomLongDateMenuItem.Checked = True

        ' Uncheck the other menu options
        If BottomDayMenuItem.Checked Then BottomDayMenuItem.Checked = False
        If BottomShortDayMenuItem.Checked Then BottomShortDayMenuItem.Checked = False
        If BottomMediumDateMenuItem.Checked Then BottomMediumDateMenuItem.Checked = False
        If BottomShortDateMenuItem.Checked Then BottomShortDateMenuItem.Checked = False
        If BottomTimeZoneMenuItem.Checked Then BottomTimeZoneMenuItem.Checked = False
        If BottomTimeZoneCityMenuItem.Checked Then BottomTimeZoneCityMenuItem.Checked = False
        If BottomLocalTimeMenuItem.Checked Then BottomLocalTimeMenuItem.Checked = False

    End Sub

    Private Sub BottomMediumDateMenuItem_Click(sender As Object, e As EventArgs) Handles BottomMediumDateMenuItem.Click

        If Not BottomDisplay.Type = InfoType.MedDate Then BottomDisplay.Type = InfoType.MedDate

        If Not BottomMediumDateMenuItem.Checked Then BottomMediumDateMenuItem.Checked = True

        ' Uncheck the other menu options
        If BottomDayMenuItem.Checked Then BottomDayMenuItem.Checked = False
        If BottomShortDayMenuItem.Checked Then BottomShortDayMenuItem.Checked = False
        If BottomLongDateMenuItem.Checked Then BottomLongDateMenuItem.Checked = False
        If BottomShortDateMenuItem.Checked Then BottomShortDateMenuItem.Checked = False
        If BottomTimeZoneMenuItem.Checked Then BottomTimeZoneMenuItem.Checked = False
        If BottomTimeZoneCityMenuItem.Checked Then BottomTimeZoneCityMenuItem.Checked = False
        If BottomLocalTimeMenuItem.Checked Then BottomLocalTimeMenuItem.Checked = False

    End Sub

    Private Sub BottomShortDateMenuItem_Click(sender As Object, e As EventArgs) Handles BottomShortDateMenuItem.Click

        If Not BottomDisplay.Type = InfoType.ShortDate Then BottomDisplay.Type = InfoType.ShortDate

        If Not BottomShortDateMenuItem.Checked Then BottomShortDateMenuItem.Checked = True

        ' Uncheck the other menu options
        If BottomDayMenuItem.Checked Then BottomDayMenuItem.Checked = False
        If BottomShortDayMenuItem.Checked Then BottomShortDayMenuItem.Checked = False
        If BottomLongDateMenuItem.Checked Then BottomLongDateMenuItem.Checked = False
        If BottomMediumDateMenuItem.Checked Then BottomMediumDateMenuItem.Checked = False
        If BottomTimeZoneMenuItem.Checked Then BottomTimeZoneMenuItem.Checked = False
        If BottomTimeZoneCityMenuItem.Checked Then BottomTimeZoneCityMenuItem.Checked = False
        If BottomLocalTimeMenuItem.Checked Then BottomLocalTimeMenuItem.Checked = False

    End Sub

    Private Sub BottomTimeZoneMenuItem_Click(sender As Object, e As EventArgs) Handles BottomTimeZoneMenuItem.Click

        If Not BottomDisplay.Type = InfoType.TimeZone Then BottomDisplay.Type = InfoType.TimeZone

        If Not BottomTimeZoneMenuItem.Checked Then BottomTimeZoneMenuItem.Checked = True

        ' Uncheck the other menu options
        If BottomDayMenuItem.Checked Then BottomDayMenuItem.Checked = False
        If BottomShortDayMenuItem.Checked Then BottomShortDayMenuItem.Checked = False
        If BottomLongDateMenuItem.Checked Then BottomLongDateMenuItem.Checked = False
        If BottomMediumDateMenuItem.Checked Then BottomMediumDateMenuItem.Checked = False
        If BottomShortDateMenuItem.Checked Then BottomShortDateMenuItem.Checked = False
        If BottomTimeZoneCityMenuItem.Checked Then BottomTimeZoneCityMenuItem.Checked = False
        If BottomLocalTimeMenuItem.Checked Then BottomLocalTimeMenuItem.Checked = False

    End Sub

    Private Sub BottomTimeZoneCityMenuItem_Click(sender As Object, e As EventArgs) Handles BottomTimeZoneCityMenuItem.Click

        If Not BottomDisplay.Type = InfoType.TimeZoneCity Then BottomDisplay.Type = InfoType.TimeZoneCity

        If Not BottomTimeZoneCityMenuItem.Checked Then BottomTimeZoneCityMenuItem.Checked = True

        ' Uncheck the other menu options
        If BottomDayMenuItem.Checked Then BottomDayMenuItem.Checked = False
        If BottomShortDayMenuItem.Checked Then BottomShortDayMenuItem.Checked = False
        If BottomLongDateMenuItem.Checked Then BottomLongDateMenuItem.Checked = False
        If BottomMediumDateMenuItem.Checked Then BottomMediumDateMenuItem.Checked = False
        If BottomShortDateMenuItem.Checked Then BottomShortDateMenuItem.Checked = False
        If BottomTimeZoneMenuItem.Checked Then BottomTimeZoneMenuItem.Checked = False
        If BottomLocalTimeMenuItem.Checked Then BottomLocalTimeMenuItem.Checked = False

    End Sub

    Private Sub BottomLocalTimeMenuItem_Click(sender As Object, e As EventArgs) Handles BottomLocalTimeMenuItem.Click

        If Not BottomDisplay.Type = InfoType.LocalTime Then BottomDisplay.Type = InfoType.LocalTime

        If Not BottomLocalTimeMenuItem.Checked Then BottomLocalTimeMenuItem.Checked = True

        ' Uncheck the other menu options
        If BottomDayMenuItem.Checked Then BottomDayMenuItem.Checked = False
        If BottomShortDayMenuItem.Checked Then BottomShortDayMenuItem.Checked = False
        If BottomLongDateMenuItem.Checked Then BottomLongDateMenuItem.Checked = False
        If BottomMediumDateMenuItem.Checked Then BottomMediumDateMenuItem.Checked = False
        If BottomShortDateMenuItem.Checked Then BottomShortDateMenuItem.Checked = False
        If BottomTimeZoneMenuItem.Checked Then BottomTimeZoneMenuItem.Checked = False
        If BottomTimeZoneCityMenuItem.Checked Then BottomTimeZoneCityMenuItem.Checked = False

    End Sub


End Class


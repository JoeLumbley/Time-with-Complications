# Time🕒 with Complications

This application is designed to display the current time, date, and time zone information in a user-friendly format.

This app allows users to switch between 12-hour and 24-hour time formats and customize the information displayed in the window.


![003](https://github.com/user-attachments/assets/464e862b-ac03-464c-acf0-3d03a91bfa64)





## Features

- **Dynamic Time Display**: Shows the current time in either 12-hour or 24-hour format.
- **Customizable Information**: Users can choose to display different types of information, including:
  - Long or short day of the week
  - Long, medium, short or military date formats
  - Current time zone and corresponding city
  - Local time
- **Responsive Design**: The application adjusts to changes in window size, ensuring a consistent user experience.
- **Buffered Graphics**: Utilizes buffered graphics for smooth rendering and improved performance.

## Getting Started

To run the application, clone the repository and open the solution file in Visual Studio. Build and run the project to start using the application.

## License

This project is licensed under the MIT License. See the LICENSE.txt file for more details.

![005](https://github.com/user-attachments/assets/1d30ce34-6e44-4bf7-b683-f49dbe438297)


















### Code Walkthrough

```vb
Public Class Form1
```
- This line defines a new class called `Form1`, which represents the main window of the application.

```vb
Private Context As BufferedGraphicsContext
Private Buffer As BufferedGraphics
```
- Here, we declare two private variables: `Context` to manage graphics rendering and `Buffer` to hold the graphics that will be drawn on the screen.

```vb
Private Enum HourFormat
    Twelve
    TwentyFour
End Enum
```
- This creates an enumeration named `HourFormat` with two possible values: `Twelve` for 12-hour time and `TwentyFour` for 24-hour time.

```vb
Private Hours As HourFormat = HourFormat.Twelve
```
- This line initializes a variable `Hours` to `HourFormat.Twelve`, meaning the app will start in 12-hour format.

```vb
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
    MilitaryDate
End Enum
```
- This defines another enumeration called `InfoType`, which lists different types of information that can be displayed (like time, date, and time zone).

```vb
Private Structure DisplayObject
    Public Location As Point
    Public Text As String
    Public Font As Font
    Public Type As InfoType
End Structure
```
- This creates a structure named `DisplayObject` that holds information about what to display on the screen. It includes the location of the text, the text itself, the font used, and the type of information.

```vb
Private MainDisplay As DisplayObject
Private TopDisplay As DisplayObject
Private BottomDisplay As DisplayObject
```
- These lines declare three instances of `DisplayObject` for the main display (time), top display (day/date), and bottom display (additional info).

```vb
Private ReadOnly AlineCenterMiddle As New StringFormat With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center}
```
- This creates a `StringFormat` object that centers text both horizontally and vertically.

### Form Load Event

```vb
Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
```
- This is an event handler that runs when the form is loaded.

```vb
    InitializeForm()
    InitializeBuffer()
```
- These calls initialize the form's settings and the graphics buffer.

```vb
    TopDisplay.Type = InfoType.LongDayOfWeek
    BottomDisplay.Type = InfoType.MedDate
```
- Here, we set the types of information to display in the top and bottom displays.

```vb
    Timer1.Interval = 20
    Timer1.Enabled = True
```
- This sets a timer to update the displays every 20 milliseconds and enables it.

```vb
    Debug.Print($"Program running... {Now.ToShortTimeString()}")
```
- This prints a message to the debug console showing the current time when the program starts.

### Form Resize Event

```vb
Private Sub Form1_Resize(sender As Object, e As EventArgs) Handles Me.Resize
```
- This event handler runs when the form is resized.

```vb
    If Not WindowState = FormWindowState.Minimized Then
```
- This checks if the window is not minimized.

```vb
        Dim FontSize As Integer = ClientSize.Width \ 14
```
- This calculates the font size based on the width of the form.

```vb
        MainDisplay.Font = New Font("Segoe UI", FontSize, FontStyle.Regular)
```
- This sets the font for the main display using the calculated size.

```vb
        MainDisplay.Location.X = ClientSize.Width \ 2
        MainDisplay.Location.Y = (ClientSize.Height + MenuStrip1.Height) \ 2
```
- These lines center the main display in the form.

```vb
        ' Similar code for TopDisplay and BottomDisplay...
```
- The same logic is applied to position the top and bottom displays.

```vb
        If Buffer IsNot Nothing Then
            Buffer.Dispose()
            Buffer = Nothing
        End If
```
- This checks if the buffer exists and disposes of it to free up resources.

### Timer Tick Event

```vb
Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
```
- This event runs every time the timer ticks.

```vb
    UpdateDisplays()
```
- This calls a method to update the text displayed on the screen.

```vb
    If Not WindowState = FormWindowState.Minimized Then
        Refresh() ' Calls OnPaint Sub
    End If
```
- If the window is not minimized, it refreshes the display to show updated information.

### OnPaint Event

```vb
Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
```
- This is where the drawing happens. It overrides the default paint behavior.

```vb
    If Buffer Is Nothing Then
        Buffer = Context.Allocate(e.Graphics, ClientRectangle)
    End If
```
- If the buffer does not exist, it allocates a new one.

```vb
    DrawDisplays()
    Buffer.Render(e.Graphics)
```
- Calls a method to draw the displays and then renders the buffer onto the screen.

### Update Displays Method

```vb
Private Sub UpdateDisplays()
    UpdateMainDisplay()
    UpdateTopDisplay()
    UpdateBottomDisplay()
End Sub
```
- This method updates all the display areas by calling their respective update methods.

### Draw Displays Method

```vb
Private Sub DrawDisplays()
```
- This method handles the actual drawing of the text on the screen.

```vb
    If Buffer IsNot Nothing Then
        Try
            With Buffer.Graphics
                .Clear(Color.Black) ' Clear the buffer with a black background
                .CompositingMode = Drawing2D.CompositingMode.SourceOver
                .TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias
                .SmoothingMode = Drawing2D.SmoothingMode.HighQuality
                .PixelOffsetMode = Drawing2D.PixelOffsetMode.None
                .CompositingQuality = Drawing2D.CompositingQuality.HighQuality
```
- It checks if the buffer exists, clears it with a black background, and sets various graphics quality options.

```vb
                .DrawString(MainDisplay.Text, MainDisplay.Font, Brushes.White, MainDisplay.Location, AlineCenterMiddle)
                .DrawString(TopDisplay.Text, TopDisplay.Font, Brushes.LightGray, TopDisplay.Location, AlineCenterMiddle)
                .DrawString(BottomDisplay.Text, BottomDisplay.Font, Brushes.LightGray, BottomDisplay.Location, AlineCenterMiddle)
```
- This draws the main, top, and bottom display texts using their respective fonts and colors.

### Update Bottom Display Method

```vb
Private Sub UpdateBottomDisplay()
    Select Case BottomDisplay.Type
```
- This method updates the text for the bottom display based on its type.

```vb
    Case InfoType.LongDayOfWeek
        BottomDisplay.Text = Now.DayOfWeek.ToString
    ' Other cases for different types...
    End Select
End Sub
```
- Depending on the type of information, it sets the text to the current day of the week, date, time zone, etc.

### Conclusion

This code creates a simple time display application that updates in real-time. It uses Windows Forms and VB.NET to manage graphics and handle user interactions. Each part of the code is designed to ensure that the display updates smoothly and provides relevant information to the user. 














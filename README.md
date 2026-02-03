# OpenMeter

A lightweight, always-on-top network speed monitor for Windows that displays real-time download and upload speeds in a draggable overlay window.

## Features

? **Real-Time Monitoring** - Displays live network download and upload speeds  
?? **Always On Top** - Stays visible above all windows  
??? **Draggable** - Position anywhere on your screen  
?? **Clean Design** - Minimal, dark-themed overlay with color-coded speeds  
?? **Unit Selection** - Switch between MB/s and KB/s  
?? **System Tray Integration** - Right-click menu for settings and exit  
?? **Lightweight** - Minimal resource usage  

## Screenshots

The overlay displays:
- **Green** Å´ Download speed (e.g., `Å´ 0.12 MB/s`)
- **Red** Å™ Upload speed (e.g., `Å™ 0.02 MB/s`)

## Requirements

- Windows 10/11
- .NET 10.0 Runtime

## Installation

1. Download the latest release from the [Releases](https://github.com/VivekNeer/OpenMeter/releases) page
2. Extract the ZIP file
3. Run `OpenMeter.exe`

## Building from Source

```bash
git clone https://github.com/VivekNeer/OpenMeter.git
cd OpenMeter
dotnet build
```

## Usage

1. **Launch** - Run `OpenMeter.exe`
2. **Position** - The overlay appears centered on screen. Click and drag to reposition
3. **Settings** - Right-click the system tray icon Å® Settings to change units (MB/s or KB/s)
4. **Exit** - Right-click the system tray icon Å® Exit

## Features Details

### Always-On-Top Overlay
The network speed display stays visible on top of all applications, perfect for monitoring while gaming, downloading, or streaming.

### Draggable Window
Click anywhere on the overlay and drag it to your preferred position on screen. The position is preserved across the session.

### Auto-Detection
Automatically detects and monitors your active network adapter (the one with the most traffic).

### Single Instance
Only one instance of OpenMeter can run at a time to prevent conflicts.

## Configuration

- **Speed Units**: Right-click tray icon Å® Settings Å® Choose KB/s or MB/s
- **Window Position**: Drag the overlay to your preferred location

## Technical Details

- Built with .NET 10.0 and Windows Forms
- Uses Windows Performance Counters for accurate network monitoring
- Updates every 1 second
- Minimal CPU and memory footprint

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## License

This project is licensed under the MIT License - see the [LICENSE.txt](LICENSE.txt) file for details.

## Author

**Vivek Neer**  
GitHub: [@VivekNeer](https://github.com/VivekNeer)

## Acknowledgments

- Inspired by various network monitoring tools
- Built with ?? for the Windows community

---

? If you find this useful, please consider giving it a star on GitHub!
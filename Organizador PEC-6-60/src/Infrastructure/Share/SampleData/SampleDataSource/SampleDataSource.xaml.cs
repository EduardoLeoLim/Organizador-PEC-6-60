//      *********    请勿修改此文件     *********
//      此文件由设计工具再生成。更改
//      此文件可能会导致错误。

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Organizador_PEC_6_60.Infrastructure.Share.SampleData.SampleDataSource;
// 若要在生产应用程序中显著减小示例数据涉及面，则可以设置
// DISABLE_SAMPLE_DATA 条件编译常量并在运行时禁用示例数据。
#if DISABLE_SAMPLE_DATA
	internal class SampleDataSource { }
#else

public class SampleDataSource : INotifyPropertyChanged
{
    public SampleDataSource()
    {
        try
        {
            var resourceUri = new Uri("/BootstrapWpfStyle;component/SampleData/SampleDataSource/SampleDataSource.xaml",
                UriKind.RelativeOrAbsolute);
            System.Windows.Application.LoadComponent(this, resourceUri);
        }
        catch
        {
        }
    }

    public ItemCollection Collection { get; } = new();

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
    }
}

public class Item : INotifyPropertyChanged
{
    private string _Property1 = string.Empty;

    private string _Property2 = string.Empty;

    private double _Property3;

    public string Property1
    {
        get => _Property1;

        set
        {
            if (_Property1 != value)
            {
                _Property1 = value;
                OnPropertyChanged("Property1");
            }
        }
    }

    public string Property2
    {
        get => _Property2;

        set
        {
            if (_Property2 != value)
            {
                _Property2 = value;
                OnPropertyChanged("Property2");
            }
        }
    }

    public double Property3
    {
        get => _Property3;

        set
        {
            if (_Property3 != value)
            {
                _Property3 = value;
                OnPropertyChanged("Property3");
            }
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
    }
}

public class ItemCollection : ObservableCollection<Item>
{
}
#endif
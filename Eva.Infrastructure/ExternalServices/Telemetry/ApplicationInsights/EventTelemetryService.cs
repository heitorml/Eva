using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;

public interface IEventTelemetryService
{
    void TrackEvent(string name, IDictionary<string,string>? props = null, IDictionary<string,double>? metrics = null);
    void TrackException(Exception ex, IDictionary<string,string>? props = null);
}

public class EventTelemetryService : IEventTelemetryService
{
    private readonly TelemetryClient _telemetryClient;
    public EventTelemetryService(TelemetryClient telemetryClient) => _telemetryClient = telemetryClient;

    public void TrackEvent(string name, IDictionary<string,string>? props = null, IDictionary<string,double>? metrics = null)
    {
        var et = new EventTelemetry(name);
        if (props != null) foreach (var kv in props) et.Properties[kv.Key] = kv.Value;
        if (metrics != null) foreach (var kv in metrics) et.Metrics[kv.Key] = kv.Value;
        _telemetryClient.TrackEvent(et);
    }

    public void TrackException(Exception ex, IDictionary<string,string>? props = null)
    {
        var et = new ExceptionTelemetry(ex);
        if (props != null) foreach (var kv in props) et.Properties[kv.Key] = kv.Value;
        _telemetryClient.TrackException(et);
    }
}   
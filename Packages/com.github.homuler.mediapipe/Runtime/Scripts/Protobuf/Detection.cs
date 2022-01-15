// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: mediapipe/framework/formats/detection.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Mediapipe {

  /// <summary>Holder for reflection information generated from mediapipe/framework/formats/detection.proto</summary>
  public static partial class DetectionReflection {

    #region Descriptor
    /// <summary>File descriptor for mediapipe/framework/formats/detection.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static DetectionReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "CittZWRpYXBpcGUvZnJhbWV3b3JrL2Zvcm1hdHMvZGV0ZWN0aW9uLnByb3Rv",
            "EgltZWRpYXBpcGUaL21lZGlhcGlwZS9mcmFtZXdvcmsvZm9ybWF0cy9sb2Nh",
            "dGlvbl9kYXRhLnByb3RvIt4CCglEZXRlY3Rpb24SDQoFbGFiZWwYASADKAkS",
            "FAoIbGFiZWxfaWQYAiADKAVCAhABEhEKBXNjb3JlGAMgAygCQgIQARIuCg1s",
            "b2NhdGlvbl9kYXRhGAQgASgLMhcubWVkaWFwaXBlLkxvY2F0aW9uRGF0YRIT",
            "CgtmZWF0dXJlX3RhZxgFIAEoCRIQCgh0cmFja19pZBgGIAEoCRIUCgxkZXRl",
            "Y3Rpb25faWQYByABKAMSRwoVYXNzb2NpYXRlZF9kZXRlY3Rpb25zGAggAygL",
            "MigubWVkaWFwaXBlLkRldGVjdGlvbi5Bc3NvY2lhdGVkRGV0ZWN0aW9uEhQK",
            "DGRpc3BsYXlfbmFtZRgJIAMoCRIWCg50aW1lc3RhbXBfdXNlYxgKIAEoAxo1",
            "ChNBc3NvY2lhdGVkRGV0ZWN0aW9uEgoKAmlkGAEgASgFEhIKCmNvbmZpZGVu",
            "Y2UYAiABKAIiOAoNRGV0ZWN0aW9uTGlzdBInCglkZXRlY3Rpb24YASADKAsy",
            "FC5tZWRpYXBpcGUuRGV0ZWN0aW9uQjQKImNvbS5nb29nbGUubWVkaWFwaXBl",
            "LmZvcm1hdHMucHJvdG9CDkRldGVjdGlvblByb3Rv"));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::Mediapipe.LocationDataReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::Mediapipe.Detection), global::Mediapipe.Detection.Parser, new[]{ "Label", "LabelId", "Score", "LocationData", "FeatureTag", "TrackId", "DetectionId", "AssociatedDetections", "DisplayName", "TimestampUsec" }, null, null, null, new pbr::GeneratedClrTypeInfo[] { new pbr::GeneratedClrTypeInfo(typeof(global::Mediapipe.Detection.Types.AssociatedDetection), global::Mediapipe.Detection.Types.AssociatedDetection.Parser, new[]{ "Id", "Confidence" }, null, null, null, null)}),
            new pbr::GeneratedClrTypeInfo(typeof(global::Mediapipe.DetectionList), global::Mediapipe.DetectionList.Parser, new[]{ "Detection" }, null, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class Detection : pb::IMessage<Detection>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<Detection> _parser = new pb::MessageParser<Detection>(() => new Detection());
    private pb::UnknownFieldSet _unknownFields;
    private int _hasBits0;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<Detection> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Mediapipe.DetectionReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Detection() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Detection(Detection other) : this() {
      _hasBits0 = other._hasBits0;
      label_ = other.label_.Clone();
      labelId_ = other.labelId_.Clone();
      score_ = other.score_.Clone();
      locationData_ = other.locationData_ != null ? other.locationData_.Clone() : null;
      featureTag_ = other.featureTag_;
      trackId_ = other.trackId_;
      detectionId_ = other.detectionId_;
      associatedDetections_ = other.associatedDetections_.Clone();
      displayName_ = other.displayName_.Clone();
      timestampUsec_ = other.timestampUsec_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Detection Clone() {
      return new Detection(this);
    }

    /// <summary>Field number for the "label" field.</summary>
    public const int LabelFieldNumber = 1;
    private static readonly pb::FieldCodec<string> _repeated_label_codec
        = pb::FieldCodec.ForString(10);
    private readonly pbc::RepeatedField<string> label_ = new pbc::RepeatedField<string>();
    /// <summary>
    /// i-th label or label_id has a score encoded by the i-th element in score.
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::RepeatedField<string> Label {
      get { return label_; }
    }

    /// <summary>Field number for the "label_id" field.</summary>
    public const int LabelIdFieldNumber = 2;
    private static readonly pb::FieldCodec<int> _repeated_labelId_codec
        = pb::FieldCodec.ForInt32(18);
    private readonly pbc::RepeatedField<int> labelId_ = new pbc::RepeatedField<int>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::RepeatedField<int> LabelId {
      get { return labelId_; }
    }

    /// <summary>Field number for the "score" field.</summary>
    public const int ScoreFieldNumber = 3;
    private static readonly pb::FieldCodec<float> _repeated_score_codec
        = pb::FieldCodec.ForFloat(26);
    private readonly pbc::RepeatedField<float> score_ = new pbc::RepeatedField<float>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::RepeatedField<float> Score {
      get { return score_; }
    }

    /// <summary>Field number for the "location_data" field.</summary>
    public const int LocationDataFieldNumber = 4;
    private global::Mediapipe.LocationData locationData_;
    /// <summary>
    /// Location data corresponding to all detected labels above.
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Mediapipe.LocationData LocationData {
      get { return locationData_; }
      set {
        locationData_ = value;
      }
    }

    /// <summary>Field number for the "feature_tag" field.</summary>
    public const int FeatureTagFieldNumber = 5;
    private readonly static string FeatureTagDefaultValue = "";

    private string featureTag_;
    /// <summary>
    /// Optional string to indicate the feature generation method. Useful in
    /// associating a name to the pipeline used to generate this detection.
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string FeatureTag {
      get { return featureTag_ ?? FeatureTagDefaultValue; }
      set {
        featureTag_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }
    /// <summary>Gets whether the "feature_tag" field is set</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool HasFeatureTag {
      get { return featureTag_ != null; }
    }
    /// <summary>Clears the value of the "feature_tag" field</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void ClearFeatureTag() {
      featureTag_ = null;
    }

    /// <summary>Field number for the "track_id" field.</summary>
    public const int TrackIdFieldNumber = 6;
    private readonly static string TrackIdDefaultValue = "";

    private string trackId_;
    /// <summary>
    /// Optional string to specify track_id if detection is part of a track.
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string TrackId {
      get { return trackId_ ?? TrackIdDefaultValue; }
      set {
        trackId_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }
    /// <summary>Gets whether the "track_id" field is set</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool HasTrackId {
      get { return trackId_ != null; }
    }
    /// <summary>Clears the value of the "track_id" field</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void ClearTrackId() {
      trackId_ = null;
    }

    /// <summary>Field number for the "detection_id" field.</summary>
    public const int DetectionIdFieldNumber = 7;
    private readonly static long DetectionIdDefaultValue = 0L;

    private long detectionId_;
    /// <summary>
    /// Optional unique id to help associate different Detections to each other.
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public long DetectionId {
      get { if ((_hasBits0 & 1) != 0) { return detectionId_; } else { return DetectionIdDefaultValue; } }
      set {
        _hasBits0 |= 1;
        detectionId_ = value;
      }
    }
    /// <summary>Gets whether the "detection_id" field is set</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool HasDetectionId {
      get { return (_hasBits0 & 1) != 0; }
    }
    /// <summary>Clears the value of the "detection_id" field</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void ClearDetectionId() {
      _hasBits0 &= ~1;
    }

    /// <summary>Field number for the "associated_detections" field.</summary>
    public const int AssociatedDetectionsFieldNumber = 8;
    private static readonly pb::FieldCodec<global::Mediapipe.Detection.Types.AssociatedDetection> _repeated_associatedDetections_codec
        = pb::FieldCodec.ForMessage(66, global::Mediapipe.Detection.Types.AssociatedDetection.Parser);
    private readonly pbc::RepeatedField<global::Mediapipe.Detection.Types.AssociatedDetection> associatedDetections_ = new pbc::RepeatedField<global::Mediapipe.Detection.Types.AssociatedDetection>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::RepeatedField<global::Mediapipe.Detection.Types.AssociatedDetection> AssociatedDetections {
      get { return associatedDetections_; }
    }

    /// <summary>Field number for the "display_name" field.</summary>
    public const int DisplayNameFieldNumber = 9;
    private static readonly pb::FieldCodec<string> _repeated_displayName_codec
        = pb::FieldCodec.ForString(74);
    private readonly pbc::RepeatedField<string> displayName_ = new pbc::RepeatedField<string>();
    /// <summary>
    /// Human-readable string for display, intended for debugging purposes. The
    /// display name corresponds to the label (or label_id). This is optional.
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::RepeatedField<string> DisplayName {
      get { return displayName_; }
    }

    /// <summary>Field number for the "timestamp_usec" field.</summary>
    public const int TimestampUsecFieldNumber = 10;
    private readonly static long TimestampUsecDefaultValue = 0L;

    private long timestampUsec_;
    /// <summary>
    /// The timestamp (in microseconds) *at which* this detection was
    /// created/detected.
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public long TimestampUsec {
      get { if ((_hasBits0 & 2) != 0) { return timestampUsec_; } else { return TimestampUsecDefaultValue; } }
      set {
        _hasBits0 |= 2;
        timestampUsec_ = value;
      }
    }
    /// <summary>Gets whether the "timestamp_usec" field is set</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool HasTimestampUsec {
      get { return (_hasBits0 & 2) != 0; }
    }
    /// <summary>Clears the value of the "timestamp_usec" field</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void ClearTimestampUsec() {
      _hasBits0 &= ~2;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as Detection);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(Detection other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if(!label_.Equals(other.label_)) return false;
      if(!labelId_.Equals(other.labelId_)) return false;
      if(!score_.Equals(other.score_)) return false;
      if (!object.Equals(LocationData, other.LocationData)) return false;
      if (FeatureTag != other.FeatureTag) return false;
      if (TrackId != other.TrackId) return false;
      if (DetectionId != other.DetectionId) return false;
      if(!associatedDetections_.Equals(other.associatedDetections_)) return false;
      if(!displayName_.Equals(other.displayName_)) return false;
      if (TimestampUsec != other.TimestampUsec) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      hash ^= label_.GetHashCode();
      hash ^= labelId_.GetHashCode();
      hash ^= score_.GetHashCode();
      if (locationData_ != null) hash ^= LocationData.GetHashCode();
      if (HasFeatureTag) hash ^= FeatureTag.GetHashCode();
      if (HasTrackId) hash ^= TrackId.GetHashCode();
      if (HasDetectionId) hash ^= DetectionId.GetHashCode();
      hash ^= associatedDetections_.GetHashCode();
      hash ^= displayName_.GetHashCode();
      if (HasTimestampUsec) hash ^= TimestampUsec.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      output.WriteRawMessage(this);
    #else
      label_.WriteTo(output, _repeated_label_codec);
      labelId_.WriteTo(output, _repeated_labelId_codec);
      score_.WriteTo(output, _repeated_score_codec);
      if (locationData_ != null) {
        output.WriteRawTag(34);
        output.WriteMessage(LocationData);
      }
      if (HasFeatureTag) {
        output.WriteRawTag(42);
        output.WriteString(FeatureTag);
      }
      if (HasTrackId) {
        output.WriteRawTag(50);
        output.WriteString(TrackId);
      }
      if (HasDetectionId) {
        output.WriteRawTag(56);
        output.WriteInt64(DetectionId);
      }
      associatedDetections_.WriteTo(output, _repeated_associatedDetections_codec);
      displayName_.WriteTo(output, _repeated_displayName_codec);
      if (HasTimestampUsec) {
        output.WriteRawTag(80);
        output.WriteInt64(TimestampUsec);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      label_.WriteTo(ref output, _repeated_label_codec);
      labelId_.WriteTo(ref output, _repeated_labelId_codec);
      score_.WriteTo(ref output, _repeated_score_codec);
      if (locationData_ != null) {
        output.WriteRawTag(34);
        output.WriteMessage(LocationData);
      }
      if (HasFeatureTag) {
        output.WriteRawTag(42);
        output.WriteString(FeatureTag);
      }
      if (HasTrackId) {
        output.WriteRawTag(50);
        output.WriteString(TrackId);
      }
      if (HasDetectionId) {
        output.WriteRawTag(56);
        output.WriteInt64(DetectionId);
      }
      associatedDetections_.WriteTo(ref output, _repeated_associatedDetections_codec);
      displayName_.WriteTo(ref output, _repeated_displayName_codec);
      if (HasTimestampUsec) {
        output.WriteRawTag(80);
        output.WriteInt64(TimestampUsec);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      size += label_.CalculateSize(_repeated_label_codec);
      size += labelId_.CalculateSize(_repeated_labelId_codec);
      size += score_.CalculateSize(_repeated_score_codec);
      if (locationData_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(LocationData);
      }
      if (HasFeatureTag) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(FeatureTag);
      }
      if (HasTrackId) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(TrackId);
      }
      if (HasDetectionId) {
        size += 1 + pb::CodedOutputStream.ComputeInt64Size(DetectionId);
      }
      size += associatedDetections_.CalculateSize(_repeated_associatedDetections_codec);
      size += displayName_.CalculateSize(_repeated_displayName_codec);
      if (HasTimestampUsec) {
        size += 1 + pb::CodedOutputStream.ComputeInt64Size(TimestampUsec);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(Detection other) {
      if (other == null) {
        return;
      }
      label_.Add(other.label_);
      labelId_.Add(other.labelId_);
      score_.Add(other.score_);
      if (other.locationData_ != null) {
        if (locationData_ == null) {
          LocationData = new global::Mediapipe.LocationData();
        }
        LocationData.MergeFrom(other.LocationData);
      }
      if (other.HasFeatureTag) {
        FeatureTag = other.FeatureTag;
      }
      if (other.HasTrackId) {
        TrackId = other.TrackId;
      }
      if (other.HasDetectionId) {
        DetectionId = other.DetectionId;
      }
      associatedDetections_.Add(other.associatedDetections_);
      displayName_.Add(other.displayName_);
      if (other.HasTimestampUsec) {
        TimestampUsec = other.TimestampUsec;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      input.ReadRawMessage(this);
    #else
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 10: {
            label_.AddEntriesFrom(input, _repeated_label_codec);
            break;
          }
          case 18:
          case 16: {
            labelId_.AddEntriesFrom(input, _repeated_labelId_codec);
            break;
          }
          case 26:
          case 29: {
            score_.AddEntriesFrom(input, _repeated_score_codec);
            break;
          }
          case 34: {
            if (locationData_ == null) {
              LocationData = new global::Mediapipe.LocationData();
            }
            input.ReadMessage(LocationData);
            break;
          }
          case 42: {
            FeatureTag = input.ReadString();
            break;
          }
          case 50: {
            TrackId = input.ReadString();
            break;
          }
          case 56: {
            DetectionId = input.ReadInt64();
            break;
          }
          case 66: {
            associatedDetections_.AddEntriesFrom(input, _repeated_associatedDetections_codec);
            break;
          }
          case 74: {
            displayName_.AddEntriesFrom(input, _repeated_displayName_codec);
            break;
          }
          case 80: {
            TimestampUsec = input.ReadInt64();
            break;
          }
        }
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
            break;
          case 10: {
            label_.AddEntriesFrom(ref input, _repeated_label_codec);
            break;
          }
          case 18:
          case 16: {
            labelId_.AddEntriesFrom(ref input, _repeated_labelId_codec);
            break;
          }
          case 26:
          case 29: {
            score_.AddEntriesFrom(ref input, _repeated_score_codec);
            break;
          }
          case 34: {
            if (locationData_ == null) {
              LocationData = new global::Mediapipe.LocationData();
            }
            input.ReadMessage(LocationData);
            break;
          }
          case 42: {
            FeatureTag = input.ReadString();
            break;
          }
          case 50: {
            TrackId = input.ReadString();
            break;
          }
          case 56: {
            DetectionId = input.ReadInt64();
            break;
          }
          case 66: {
            associatedDetections_.AddEntriesFrom(ref input, _repeated_associatedDetections_codec);
            break;
          }
          case 74: {
            displayName_.AddEntriesFrom(ref input, _repeated_displayName_codec);
            break;
          }
          case 80: {
            TimestampUsec = input.ReadInt64();
            break;
          }
        }
      }
    }
    #endif

    #region Nested types
    /// <summary>Container for nested types declared in the Detection message type.</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static partial class Types {
      /// <summary>
      /// Useful for associating a detection with other detections based on the
      /// detection_id. For example, this could be used to associate a face detection
      /// with a body detection when they belong to the same person.
      /// </summary>
      public sealed partial class AssociatedDetection : pb::IMessage<AssociatedDetection>
      #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
          , pb::IBufferMessage
      #endif
      {
        private static readonly pb::MessageParser<AssociatedDetection> _parser = new pb::MessageParser<AssociatedDetection>(() => new AssociatedDetection());
        private pb::UnknownFieldSet _unknownFields;
        private int _hasBits0;
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public static pb::MessageParser<AssociatedDetection> Parser { get { return _parser; } }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public static pbr::MessageDescriptor Descriptor {
          get { return global::Mediapipe.Detection.Descriptor.NestedTypes[0]; }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        pbr::MessageDescriptor pb::IMessage.Descriptor {
          get { return Descriptor; }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public AssociatedDetection() {
          OnConstruction();
        }

        partial void OnConstruction();

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public AssociatedDetection(AssociatedDetection other) : this() {
          _hasBits0 = other._hasBits0;
          id_ = other.id_;
          confidence_ = other.confidence_;
          _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public AssociatedDetection Clone() {
          return new AssociatedDetection(this);
        }

        /// <summary>Field number for the "id" field.</summary>
        public const int IdFieldNumber = 1;
        private readonly static int IdDefaultValue = 0;

        private int id_;
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public int Id {
          get { if ((_hasBits0 & 1) != 0) { return id_; } else { return IdDefaultValue; } }
          set {
            _hasBits0 |= 1;
            id_ = value;
          }
        }
        /// <summary>Gets whether the "id" field is set</summary>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public bool HasId {
          get { return (_hasBits0 & 1) != 0; }
        }
        /// <summary>Clears the value of the "id" field</summary>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public void ClearId() {
          _hasBits0 &= ~1;
        }

        /// <summary>Field number for the "confidence" field.</summary>
        public const int ConfidenceFieldNumber = 2;
        private readonly static float ConfidenceDefaultValue = 0F;

        private float confidence_;
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public float Confidence {
          get { if ((_hasBits0 & 2) != 0) { return confidence_; } else { return ConfidenceDefaultValue; } }
          set {
            _hasBits0 |= 2;
            confidence_ = value;
          }
        }
        /// <summary>Gets whether the "confidence" field is set</summary>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public bool HasConfidence {
          get { return (_hasBits0 & 2) != 0; }
        }
        /// <summary>Clears the value of the "confidence" field</summary>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public void ClearConfidence() {
          _hasBits0 &= ~2;
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public override bool Equals(object other) {
          return Equals(other as AssociatedDetection);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public bool Equals(AssociatedDetection other) {
          if (ReferenceEquals(other, null)) {
            return false;
          }
          if (ReferenceEquals(other, this)) {
            return true;
          }
          if (Id != other.Id) return false;
          if (!pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.Equals(Confidence, other.Confidence)) return false;
          return Equals(_unknownFields, other._unknownFields);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public override int GetHashCode() {
          int hash = 1;
          if (HasId) hash ^= Id.GetHashCode();
          if (HasConfidence) hash ^= pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.GetHashCode(Confidence);
          if (_unknownFields != null) {
            hash ^= _unknownFields.GetHashCode();
          }
          return hash;
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public override string ToString() {
          return pb::JsonFormatter.ToDiagnosticString(this);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public void WriteTo(pb::CodedOutputStream output) {
        #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
          output.WriteRawMessage(this);
        #else
          if (HasId) {
            output.WriteRawTag(8);
            output.WriteInt32(Id);
          }
          if (HasConfidence) {
            output.WriteRawTag(21);
            output.WriteFloat(Confidence);
          }
          if (_unknownFields != null) {
            _unknownFields.WriteTo(output);
          }
        #endif
        }

        #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
          if (HasId) {
            output.WriteRawTag(8);
            output.WriteInt32(Id);
          }
          if (HasConfidence) {
            output.WriteRawTag(21);
            output.WriteFloat(Confidence);
          }
          if (_unknownFields != null) {
            _unknownFields.WriteTo(ref output);
          }
        }
        #endif

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public int CalculateSize() {
          int size = 0;
          if (HasId) {
            size += 1 + pb::CodedOutputStream.ComputeInt32Size(Id);
          }
          if (HasConfidence) {
            size += 1 + 4;
          }
          if (_unknownFields != null) {
            size += _unknownFields.CalculateSize();
          }
          return size;
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public void MergeFrom(AssociatedDetection other) {
          if (other == null) {
            return;
          }
          if (other.HasId) {
            Id = other.Id;
          }
          if (other.HasConfidence) {
            Confidence = other.Confidence;
          }
          _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public void MergeFrom(pb::CodedInputStream input) {
        #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
          input.ReadRawMessage(this);
        #else
          uint tag;
          while ((tag = input.ReadTag()) != 0) {
            switch(tag) {
              default:
                _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
                break;
              case 8: {
                Id = input.ReadInt32();
                break;
              }
              case 21: {
                Confidence = input.ReadFloat();
                break;
              }
            }
          }
        #endif
        }

        #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
          uint tag;
          while ((tag = input.ReadTag()) != 0) {
            switch(tag) {
              default:
                _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
                break;
              case 8: {
                Id = input.ReadInt32();
                break;
              }
              case 21: {
                Confidence = input.ReadFloat();
                break;
              }
            }
          }
        }
        #endif

      }

    }
    #endregion

  }

  /// <summary>
  /// Group of Detection protos.
  /// </summary>
  public sealed partial class DetectionList : pb::IMessage<DetectionList>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<DetectionList> _parser = new pb::MessageParser<DetectionList>(() => new DetectionList());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<DetectionList> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Mediapipe.DetectionReflection.Descriptor.MessageTypes[1]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public DetectionList() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public DetectionList(DetectionList other) : this() {
      detection_ = other.detection_.Clone();
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public DetectionList Clone() {
      return new DetectionList(this);
    }

    /// <summary>Field number for the "detection" field.</summary>
    public const int DetectionFieldNumber = 1;
    private static readonly pb::FieldCodec<global::Mediapipe.Detection> _repeated_detection_codec
        = pb::FieldCodec.ForMessage(10, global::Mediapipe.Detection.Parser);
    private readonly pbc::RepeatedField<global::Mediapipe.Detection> detection_ = new pbc::RepeatedField<global::Mediapipe.Detection>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::RepeatedField<global::Mediapipe.Detection> Detection {
      get { return detection_; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as DetectionList);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(DetectionList other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if(!detection_.Equals(other.detection_)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      hash ^= detection_.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      output.WriteRawMessage(this);
    #else
      detection_.WriteTo(output, _repeated_detection_codec);
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      detection_.WriteTo(ref output, _repeated_detection_codec);
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      size += detection_.CalculateSize(_repeated_detection_codec);
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(DetectionList other) {
      if (other == null) {
        return;
      }
      detection_.Add(other.detection_);
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      input.ReadRawMessage(this);
    #else
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 10: {
            detection_.AddEntriesFrom(input, _repeated_detection_codec);
            break;
          }
        }
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
            break;
          case 10: {
            detection_.AddEntriesFrom(ref input, _repeated_detection_codec);
            break;
          }
        }
      }
    }
    #endif

  }

  #endregion

}

#endregion Designer generated code

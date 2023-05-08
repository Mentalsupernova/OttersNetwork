from google.protobuf.internal import containers as _containers
from google.protobuf import descriptor as _descriptor
from google.protobuf import message as _message
from typing import ClassVar as _ClassVar, Iterable as _Iterable, Mapping as _Mapping, Optional as _Optional, Union as _Union

DESCRIPTOR: _descriptor.FileDescriptor

class Empty(_message.Message):
    __slots__ = []
    def __init__(self) -> None: ...

class MessageUrlRecord(_message.Message):
    __slots__ = ["colorScheme", "description", "id", "keyWords", "mood", "url", "websiteType"]
    COLORSCHEME_FIELD_NUMBER: _ClassVar[int]
    DESCRIPTION_FIELD_NUMBER: _ClassVar[int]
    ID_FIELD_NUMBER: _ClassVar[int]
    KEYWORDS_FIELD_NUMBER: _ClassVar[int]
    MOOD_FIELD_NUMBER: _ClassVar[int]
    URL_FIELD_NUMBER: _ClassVar[int]
    WEBSITETYPE_FIELD_NUMBER: _ClassVar[int]
    colorScheme: str
    description: str
    id: int
    keyWords: str
    mood: str
    url: str
    websiteType: str
    def __init__(self, id: _Optional[int] = ..., url: _Optional[str] = ..., description: _Optional[str] = ..., keyWords: _Optional[str] = ..., websiteType: _Optional[str] = ..., mood: _Optional[str] = ..., colorScheme: _Optional[str] = ...) -> None: ...

class MessageUrlRecordImage(_message.Message):
    __slots__ = ["RecordId", "id", "images", "url"]
    ID_FIELD_NUMBER: _ClassVar[int]
    IMAGES_FIELD_NUMBER: _ClassVar[int]
    RECORDID_FIELD_NUMBER: _ClassVar[int]
    RecordId: int
    URL_FIELD_NUMBER: _ClassVar[int]
    id: int
    images: bytes
    url: str
    def __init__(self, id: _Optional[int] = ..., RecordId: _Optional[int] = ..., url: _Optional[str] = ..., images: _Optional[bytes] = ...) -> None: ...

class MessageUrlRecordStream(_message.Message):
    __slots__ = ["records"]
    RECORDS_FIELD_NUMBER: _ClassVar[int]
    records: _containers.RepeatedCompositeFieldContainer[MessageUrlRecord]
    def __init__(self, records: _Optional[_Iterable[_Union[MessageUrlRecord, _Mapping]]] = ...) -> None: ...

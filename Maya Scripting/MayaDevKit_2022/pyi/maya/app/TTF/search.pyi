from typing import Any, Container, Dict, Generic, Iterable, Iterator, List, Optional, Set, Tuple, TypeVar, Union
import re


if False:
    from typing import Dict, List, Tuple, Union, Optional

class TTFSearch:
    def __init__(self): ...
    def addItem(self, item): ...
    def find(self, searchString): ...


class ClosestMatch:
    def __init__(self, values, maxErr='2'): ...
    def search(self, text): ...
    def searchChildren(self, node, errors): ...
    def searchRecursive(self, node, x, prevErrors): ...


class ClosestMatchNode:
    def __init__(self): ...
    def add(self, text): ...



﻿using System.Reflection;

namespace We.Csv;
internal record MapColumn(int Index, string Name, PropertyInfo Property);


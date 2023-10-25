﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation.Metadata;

namespace RegistrationModule.Other
{
    [DefaultValue(AlarmStatus.CorrectData)]
    public enum AlarmStatus
    {
        CorrectData,
        IncorrectData,
        IncorrectUUID
    }
}

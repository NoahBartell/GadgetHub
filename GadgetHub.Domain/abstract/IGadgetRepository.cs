﻿using System;
using System.Collections.Generic;
using GadgetHub.Domain.Entities;

namespace GadgetHub.Domain.Abstract
{
    public interface IGadgetRepository
    {
        IEnumerable<Gadget> Gadgets { get; }

        void SaveGadget(Gadget gadgetid);

        Gadget DeleteGadget(int gadgetid);
    }
}

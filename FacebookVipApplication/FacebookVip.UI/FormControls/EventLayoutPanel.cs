﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using FacebookVip.Logic.Interfaces;
using FacebookVip.Logic.Services;
using FacebookWrapper.ObjectModel;

namespace FacebookVip.UI.FormControls
{
    internal class EventLayoutPanel : ILayoutPanel
    {
        private TableLayoutPanel m_Panel;

        public async Task<TableLayoutPanel> GetLayoutAsync(User i_LoggedInUser)
        {
            IEventService eventService = new EventService();
            List<Event> userEvents;
            try
            {
                userEvents = await eventService.GetUserEventsAsync(i_LoggedInUser);
            }
            catch(Exception)
            {
                //NO PERMISSION! - mock data
                userEvents = new List<Event>();
                for(int i = 0; i < 50; i++)
                {
                    userEvents.Add(new Event{Description = "Event " + i});
                }
            }

            m_Panel = new TableLayoutPanel
                          {
                              ColumnCount = 1,
                              AutoScroll = true,
                              AutoSize = true,
                              CellBorderStyle = TableLayoutPanelCellBorderStyle.Single,
                              Padding = new Padding(10, 0, 10, 0)
                          };


            foreach (Event eventItem in userEvents)
            {
                m_Panel.Controls.Add(new Label { Font = new Font("Arial", 12, FontStyle.Bold), Text = eventItem.Description });
            }

            return m_Panel;
        }
    }
}


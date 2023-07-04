﻿// <copyright file="LanguagePutRequestExample.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using OneFrameCGPTPlayground.WebAPI.Model.Language;
using Swashbuckle.AspNetCore.Filters;

namespace OneFrameCGPTPlayground.WebAPI.Examples.Response.Language
{
    public class LanguagePutRequestExample : IExamplesProvider<LanguagePutRequest>
    {
        public LanguagePutRequest GetExamples()
        {
            return new LanguagePutRequest()
            {
                Id = new Guid("5a41be5e-0cb9-4a3e-a1a7-0244b53134cc"),
                Name = "English",
                Code = "en-EN",
                IsDefault = true,
                IsActive = true,
                Image = "data:image/svg+xml;base64,PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0iaXNvLTg4NTktMSI/Pgo8IS0tIEdlbmVyYXRvcjogQWRvYmUgSWxsdXN0cmF0b3IgMTkuMC4wLCBTVkcgRXhwb3J0IFBsdWctSW4gLiBTVkcgVmVyc2lvbjogNi4wMCBCdWlsZCAwKSAgLS0+CjxzdmcgeG1sbnM9Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvc3ZnIiB4bWxuczp4bGluaz0iaHR0cDovL3d3dy53My5vcmcvMTk5OS94bGluayIgdmVyc2lvbj0iMS4xIiBpZD0iTGF5ZXJfMSIgeD0iMHB4IiB5PSIwcHgiIHZpZXdCb3g9IjAgMCA1MTIgNTEyIiBzdHlsZT0iZW5hYmxlLWJhY2tncm91bmQ6bmV3IDAgMCA1MTIgNTEyOyIgeG1sOnNwYWNlPSJwcmVzZXJ2ZSI+CjxnPgoJPHBvbHlnb24gc3R5bGU9ImZpbGw6IzQxNDc5QjsiIHBvaW50cz0iMTg4LjYzMiwwIDAsMCAwLDM5Ljk1NCAxODguNjMyLDE2My41NCAgIi8+Cgk8cG9seWdvbiBzdHlsZT0iZmlsbDojNDE0NzlCOyIgcG9pbnRzPSIwLDEzNi41OTggMCwxODguNjMyIDc5LjQxOSwxODguNjMyICAiLz4KCTxwb2x5Z29uIHN0eWxlPSJmaWxsOiM0MTQ3OUI7IiBwb2ludHM9IjAsMzIzLjM2OSAwLDM3NS40MDIgNzkuNDE5LDMyMy4zNjkgICIvPgoJPHBvbHlnb24gc3R5bGU9ImZpbGw6IzQxNDc5QjsiIHBvaW50cz0iNTEyLDM5Ljk1NCA1MTIsMCAzMjMuMzY4LDAgMzIzLjM2OCwxNjMuNTQgICIvPgoJPHBvbHlnb24gc3R5bGU9ImZpbGw6IzQxNDc5QjsiIHBvaW50cz0iNTEyLDM3NS40MDIgNTEyLDMyMy4zNjkgNDMyLjU4MSwzMjMuMzY5ICAiLz4KCTxwb2x5Z29uIHN0eWxlPSJmaWxsOiM0MTQ3OUI7IiBwb2ludHM9IjMyMy4zNjgsMzQ4LjQ2IDMyMy4zNjgsNTEyIDUxMiw1MTIgNTEyLDQ3Mi4wNDYgICIvPgoJPHBvbHlnb24gc3R5bGU9ImZpbGw6IzQxNDc5QjsiIHBvaW50cz0iNTEyLDE4OC42MzIgNTEyLDEzNi41OTggNDMyLjU4MSwxODguNjMyICAiLz4KCTxwb2x5Z29uIHN0eWxlPSJmaWxsOiM0MTQ3OUI7IiBwb2ludHM9IjAsNDcyLjA0NiAwLDUxMiAxODguNjMyLDUxMiAxODguNjMyLDM0OC40NiAgIi8+CjwvZz4KPGc+Cgk8cG9seWdvbiBzdHlsZT0iZmlsbDojRjVGNUY1OyIgcG9pbnRzPSI1MTIsNDcyLjA0NiA1MTIsNDMyLjExOCAzNDQuNDY1LDMyMy4zNjkgMzkzLjk2MSwzMjMuMzY5IDUxMiwzOTkuOTg5IDUxMiwzNzUuNDAyICAgIDQzMi41ODEsMzIzLjM2OSA1MTIsMzIzLjM2OSA1MTIsMjk2LjQyMSAyOTYuNDIxLDI5Ni40MjEgMjk2LjQyMSw1MTIgMzIzLjM2OCw1MTIgMzIzLjM2OCwzNDguNDYgICIvPgoJPHBvbHlnb24gc3R5bGU9ImZpbGw6I0Y1RjVGNTsiIHBvaW50cz0iMCwyOTYuNDIxIDAsMzIzLjM2OSA3OS40MTksMzIzLjM2OSAwLDM3NS40MDIgMCw0MTMuMjAzIDEzOC4zOTUsMzIzLjM2OSAxODcuODkxLDMyMy4zNjkgICAgMCw0NDUuMzMyIDAsNDcyLjA0NiAxODguNjMyLDM0OC40NiAxODguNjMyLDUxMiAyMTUuNTc5LDUxMiAyMTUuNTc5LDI5Ni40MjEgICIvPgoJPHBvbHlnb24gc3R5bGU9ImZpbGw6I0Y1RjVGNTsiIHBvaW50cz0iMjE1LjU3OSwwIDE4OC42MzIsMCAxODguNjMyLDE2My41NCAwLDM5Ljk1NCAwLDgzLjY3OSAxNjEuNjg0LDE4OC42MzIgMTEyLjE4OCwxODguNjMyICAgIDAsMTE1LjgwNyAwLDEzNi41OTggNzkuNDE5LDE4OC42MzIgMCwxODguNjMyIDAsMjE1LjU3OSAyMTUuNTc5LDIxNS41NzkgICIvPgoJPHBvbHlnb24gc3R5bGU9ImZpbGw6I0Y1RjVGNTsiIHBvaW50cz0iNTEyLDIxNS41NzkgNTEyLDE4OC42MzIgNDMyLjU4MSwxODguNjMyIDUxMiwxMzYuNTk4IDUxMiw5OC4zMTQgMzcyLjg2NCwxODguNjMyICAgIDMyMy4zNjgsMTg4LjYzMiA1MTIsNjYuMTg1IDUxMiwzOS45NTQgMzIzLjM2OCwxNjMuNTQgMzIzLjM2OCwwIDI5Ni40MjEsMCAyOTYuNDIxLDIxNS41NzkgICIvPgo8L2c+CjxnPgoJPHBvbHlnb24gc3R5bGU9ImZpbGw6I0ZGNEI1NTsiIHBvaW50cz0iNTEyLDI5Ni40MjEgNTEyLDIxNS41NzkgMjk2LjQyMSwyMTUuNTc5IDI5Ni40MjEsMCAyMTUuNTc5LDAgMjE1LjU3OSwyMTUuNTc5IDAsMjE1LjU3OSAgICAwLDI5Ni40MjEgMjE1LjU3OSwyOTYuNDIxIDIxNS41NzksNTEyIDI5Ni40MjEsNTEyIDI5Ni40MjEsMjk2LjQyMSAgIi8+Cgk8cG9seWdvbiBzdHlsZT0iZmlsbDojRkY0QjU1OyIgcG9pbnRzPSIxMzguMzk1LDMyMy4zNjkgMCw0MTMuMjAzIDAsNDQ1LjMzMiAxODcuODkxLDMyMy4zNjkgICIvPgoJPHBvbHlnb24gc3R5bGU9ImZpbGw6I0ZGNEI1NTsiIHBvaW50cz0iMzQ0LjQ2NSwzMjMuMzY5IDUxMiw0MzIuMTE4IDUxMiwzOTkuOTg5IDM5My45NjEsMzIzLjM2OSAgIi8+Cgk8cG9seWdvbiBzdHlsZT0iZmlsbDojRkY0QjU1OyIgcG9pbnRzPSIxNjEuNjg0LDE4OC42MzIgMCw4My42NzkgMCwxMTUuODA3IDExMi4xODgsMTg4LjYzMiAgIi8+Cgk8cG9seWdvbiBzdHlsZT0iZmlsbDojRkY0QjU1OyIgcG9pbnRzPSIzNzIuODY0LDE4OC42MzIgNTEyLDk4LjMxNCA1MTIsNjYuMTg1IDMyMy4zNjgsMTg4LjYzMiAgIi8+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPC9zdmc+Cg==",
            };
        }
    }
}

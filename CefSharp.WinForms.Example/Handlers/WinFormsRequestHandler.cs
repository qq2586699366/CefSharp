﻿// Copyright © 2010-2016 The CefSharp Authors. All rights reserved.
//
// Use of this source code is governed by a BSD-style license that can be found in the LICENSE file.

using System;
using System.Windows.Forms;
using CefSharp.Example;
using CefSharp.WinForms.Internals;
using System.Security.Cryptography.X509Certificates;

namespace CefSharp.WinForms.Example.Handlers
{
    public class WinFormsRequestHandler : RequestHandler
    {
        private Action<string, int?> openNewTab;

        public WinFormsRequestHandler(Action<string, int?> openNewTab)
        {
            this.openNewTab = openNewTab;
        }

        protected override bool OnOpenUrlFromTab(IWebBrowser browserControl, IBrowser browser, IFrame frame, string targetUrl, WindowOpenDisposition targetDisposition, bool userGesture)
        {
            if(openNewTab == null)
            {
                return false;
            }

            var control = (Control)browserControl;

            control.InvokeOnUiThreadIfRequired(delegate ()
            {
                openNewTab(targetUrl, null);
            });			

            return true;
        }

        protected override bool OnSelectClientCertificate(IWebBrowser browserControl, IBrowser browser, bool isProxy, string host, int port, X509Certificate2Collection certificates, ISelectClientCertificateCallback callback)
        {
            X509Certificate2Collection selectedCertificateCollection = X509Certificate2UI.SelectFromCollection(certificates, "Certificates Dialog", "Select Certificate for authentication", X509SelectionFlag.SingleSelection);
            foreach (X509Certificate2 x509 in selectedCertificateCollection)
            {
                callback.Select(x509);
            }
            return true;
        }
    }
}

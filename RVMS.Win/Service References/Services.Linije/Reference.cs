﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18046
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RVMS.Win.Services.Linije {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="Services.Linije.Linije")]
    public interface Linije {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Linije/SacuvajLiniju", ReplyAction="http://tempuri.org/Linije/SacuvajLinijuResponse")]
        int SacuvajLiniju(RVMS.Model.DTO.LinijaDTO linijaDto);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://tempuri.org/Linije/SacuvajLiniju", ReplyAction="http://tempuri.org/Linije/SacuvajLinijuResponse")]
        System.IAsyncResult BeginSacuvajLiniju(RVMS.Model.DTO.LinijaDTO linijaDto, System.AsyncCallback callback, object asyncState);
        
        int EndSacuvajLiniju(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Linije/DodajStajalisteNaLiniju", ReplyAction="http://tempuri.org/Linije/DodajStajalisteNaLinijuResponse")]
        RVMS.Model.DTO.LinijaSaKandidatimaDTO DodajStajalisteNaLiniju(int idLinije, int idStajalista);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://tempuri.org/Linije/DodajStajalisteNaLiniju", ReplyAction="http://tempuri.org/Linije/DodajStajalisteNaLinijuResponse")]
        System.IAsyncResult BeginDodajStajalisteNaLiniju(int idLinije, int idStajalista, System.AsyncCallback callback, object asyncState);
        
        RVMS.Model.DTO.LinijaSaKandidatimaDTO EndDodajStajalisteNaLiniju(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Linije/DodajStajalistaRelacijeNaLiniju", ReplyAction="http://tempuri.org/Linije/DodajStajalistaRelacijeNaLinijuResponse")]
        RVMS.Model.DTO.LinijaSaKandidatimaDTO DodajStajalistaRelacijeNaLiniju(int idLinije, int idRelacije);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://tempuri.org/Linije/DodajStajalistaRelacijeNaLiniju", ReplyAction="http://tempuri.org/Linije/DodajStajalistaRelacijeNaLinijuResponse")]
        System.IAsyncResult BeginDodajStajalistaRelacijeNaLiniju(int idLinije, int idRelacije, System.AsyncCallback callback, object asyncState);
        
        RVMS.Model.DTO.LinijaSaKandidatimaDTO EndDodajStajalistaRelacijeNaLiniju(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Linije/SkloniStajalisteSaLinije", ReplyAction="http://tempuri.org/Linije/SkloniStajalisteSaLinijeResponse")]
        RVMS.Model.DTO.LinijaSaKandidatimaDTO SkloniStajalisteSaLinije(int idLinije, int idStajalista);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://tempuri.org/Linije/SkloniStajalisteSaLinije", ReplyAction="http://tempuri.org/Linije/SkloniStajalisteSaLinijeResponse")]
        System.IAsyncResult BeginSkloniStajalisteSaLinije(int idLinije, int idStajalista, System.AsyncCallback callback, object asyncState);
        
        RVMS.Model.DTO.LinijaSaKandidatimaDTO EndSkloniStajalisteSaLinije(System.IAsyncResult result);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface LinijeChannel : RVMS.Win.Services.Linije.Linije, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class SacuvajLinijuCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public SacuvajLinijuCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public int Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((int)(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class DodajStajalisteNaLinijuCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public DodajStajalisteNaLinijuCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public RVMS.Model.DTO.LinijaSaKandidatimaDTO Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((RVMS.Model.DTO.LinijaSaKandidatimaDTO)(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class DodajStajalistaRelacijeNaLinijuCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public DodajStajalistaRelacijeNaLinijuCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public RVMS.Model.DTO.LinijaSaKandidatimaDTO Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((RVMS.Model.DTO.LinijaSaKandidatimaDTO)(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class SkloniStajalisteSaLinijeCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public SkloniStajalisteSaLinijeCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public RVMS.Model.DTO.LinijaSaKandidatimaDTO Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((RVMS.Model.DTO.LinijaSaKandidatimaDTO)(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class LinijeClient : System.ServiceModel.ClientBase<RVMS.Win.Services.Linije.Linije>, RVMS.Win.Services.Linije.Linije {
        
        private BeginOperationDelegate onBeginSacuvajLinijuDelegate;
        
        private EndOperationDelegate onEndSacuvajLinijuDelegate;
        
        private System.Threading.SendOrPostCallback onSacuvajLinijuCompletedDelegate;
        
        private BeginOperationDelegate onBeginDodajStajalisteNaLinijuDelegate;
        
        private EndOperationDelegate onEndDodajStajalisteNaLinijuDelegate;
        
        private System.Threading.SendOrPostCallback onDodajStajalisteNaLinijuCompletedDelegate;
        
        private BeginOperationDelegate onBeginDodajStajalistaRelacijeNaLinijuDelegate;
        
        private EndOperationDelegate onEndDodajStajalistaRelacijeNaLinijuDelegate;
        
        private System.Threading.SendOrPostCallback onDodajStajalistaRelacijeNaLinijuCompletedDelegate;
        
        private BeginOperationDelegate onBeginSkloniStajalisteSaLinijeDelegate;
        
        private EndOperationDelegate onEndSkloniStajalisteSaLinijeDelegate;
        
        private System.Threading.SendOrPostCallback onSkloniStajalisteSaLinijeCompletedDelegate;
        
        public LinijeClient() {
        }
        
        public LinijeClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public LinijeClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public LinijeClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public LinijeClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public event System.EventHandler<SacuvajLinijuCompletedEventArgs> SacuvajLinijuCompleted;
        
        public event System.EventHandler<DodajStajalisteNaLinijuCompletedEventArgs> DodajStajalisteNaLinijuCompleted;
        
        public event System.EventHandler<DodajStajalistaRelacijeNaLinijuCompletedEventArgs> DodajStajalistaRelacijeNaLinijuCompleted;
        
        public event System.EventHandler<SkloniStajalisteSaLinijeCompletedEventArgs> SkloniStajalisteSaLinijeCompleted;
        
        public int SacuvajLiniju(RVMS.Model.DTO.LinijaDTO linijaDto) {
            return base.Channel.SacuvajLiniju(linijaDto);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult BeginSacuvajLiniju(RVMS.Model.DTO.LinijaDTO linijaDto, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginSacuvajLiniju(linijaDto, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public int EndSacuvajLiniju(System.IAsyncResult result) {
            return base.Channel.EndSacuvajLiniju(result);
        }
        
        private System.IAsyncResult OnBeginSacuvajLiniju(object[] inValues, System.AsyncCallback callback, object asyncState) {
            RVMS.Model.DTO.LinijaDTO linijaDto = ((RVMS.Model.DTO.LinijaDTO)(inValues[0]));
            return this.BeginSacuvajLiniju(linijaDto, callback, asyncState);
        }
        
        private object[] OnEndSacuvajLiniju(System.IAsyncResult result) {
            int retVal = this.EndSacuvajLiniju(result);
            return new object[] {
                    retVal};
        }
        
        private void OnSacuvajLinijuCompleted(object state) {
            if ((this.SacuvajLinijuCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.SacuvajLinijuCompleted(this, new SacuvajLinijuCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void SacuvajLinijuAsync(RVMS.Model.DTO.LinijaDTO linijaDto) {
            this.SacuvajLinijuAsync(linijaDto, null);
        }
        
        public void SacuvajLinijuAsync(RVMS.Model.DTO.LinijaDTO linijaDto, object userState) {
            if ((this.onBeginSacuvajLinijuDelegate == null)) {
                this.onBeginSacuvajLinijuDelegate = new BeginOperationDelegate(this.OnBeginSacuvajLiniju);
            }
            if ((this.onEndSacuvajLinijuDelegate == null)) {
                this.onEndSacuvajLinijuDelegate = new EndOperationDelegate(this.OnEndSacuvajLiniju);
            }
            if ((this.onSacuvajLinijuCompletedDelegate == null)) {
                this.onSacuvajLinijuCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnSacuvajLinijuCompleted);
            }
            base.InvokeAsync(this.onBeginSacuvajLinijuDelegate, new object[] {
                        linijaDto}, this.onEndSacuvajLinijuDelegate, this.onSacuvajLinijuCompletedDelegate, userState);
        }
        
        public RVMS.Model.DTO.LinijaSaKandidatimaDTO DodajStajalisteNaLiniju(int idLinije, int idStajalista) {
            return base.Channel.DodajStajalisteNaLiniju(idLinije, idStajalista);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult BeginDodajStajalisteNaLiniju(int idLinije, int idStajalista, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginDodajStajalisteNaLiniju(idLinije, idStajalista, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public RVMS.Model.DTO.LinijaSaKandidatimaDTO EndDodajStajalisteNaLiniju(System.IAsyncResult result) {
            return base.Channel.EndDodajStajalisteNaLiniju(result);
        }
        
        private System.IAsyncResult OnBeginDodajStajalisteNaLiniju(object[] inValues, System.AsyncCallback callback, object asyncState) {
            int idLinije = ((int)(inValues[0]));
            int idStajalista = ((int)(inValues[1]));
            return this.BeginDodajStajalisteNaLiniju(idLinije, idStajalista, callback, asyncState);
        }
        
        private object[] OnEndDodajStajalisteNaLiniju(System.IAsyncResult result) {
            RVMS.Model.DTO.LinijaSaKandidatimaDTO retVal = this.EndDodajStajalisteNaLiniju(result);
            return new object[] {
                    retVal};
        }
        
        private void OnDodajStajalisteNaLinijuCompleted(object state) {
            if ((this.DodajStajalisteNaLinijuCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.DodajStajalisteNaLinijuCompleted(this, new DodajStajalisteNaLinijuCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void DodajStajalisteNaLinijuAsync(int idLinije, int idStajalista) {
            this.DodajStajalisteNaLinijuAsync(idLinije, idStajalista, null);
        }
        
        public void DodajStajalisteNaLinijuAsync(int idLinije, int idStajalista, object userState) {
            if ((this.onBeginDodajStajalisteNaLinijuDelegate == null)) {
                this.onBeginDodajStajalisteNaLinijuDelegate = new BeginOperationDelegate(this.OnBeginDodajStajalisteNaLiniju);
            }
            if ((this.onEndDodajStajalisteNaLinijuDelegate == null)) {
                this.onEndDodajStajalisteNaLinijuDelegate = new EndOperationDelegate(this.OnEndDodajStajalisteNaLiniju);
            }
            if ((this.onDodajStajalisteNaLinijuCompletedDelegate == null)) {
                this.onDodajStajalisteNaLinijuCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnDodajStajalisteNaLinijuCompleted);
            }
            base.InvokeAsync(this.onBeginDodajStajalisteNaLinijuDelegate, new object[] {
                        idLinije,
                        idStajalista}, this.onEndDodajStajalisteNaLinijuDelegate, this.onDodajStajalisteNaLinijuCompletedDelegate, userState);
        }
        
        public RVMS.Model.DTO.LinijaSaKandidatimaDTO DodajStajalistaRelacijeNaLiniju(int idLinije, int idRelacije) {
            return base.Channel.DodajStajalistaRelacijeNaLiniju(idLinije, idRelacije);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult BeginDodajStajalistaRelacijeNaLiniju(int idLinije, int idRelacije, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginDodajStajalistaRelacijeNaLiniju(idLinije, idRelacije, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public RVMS.Model.DTO.LinijaSaKandidatimaDTO EndDodajStajalistaRelacijeNaLiniju(System.IAsyncResult result) {
            return base.Channel.EndDodajStajalistaRelacijeNaLiniju(result);
        }
        
        private System.IAsyncResult OnBeginDodajStajalistaRelacijeNaLiniju(object[] inValues, System.AsyncCallback callback, object asyncState) {
            int idLinije = ((int)(inValues[0]));
            int idRelacije = ((int)(inValues[1]));
            return this.BeginDodajStajalistaRelacijeNaLiniju(idLinije, idRelacije, callback, asyncState);
        }
        
        private object[] OnEndDodajStajalistaRelacijeNaLiniju(System.IAsyncResult result) {
            RVMS.Model.DTO.LinijaSaKandidatimaDTO retVal = this.EndDodajStajalistaRelacijeNaLiniju(result);
            return new object[] {
                    retVal};
        }
        
        private void OnDodajStajalistaRelacijeNaLinijuCompleted(object state) {
            if ((this.DodajStajalistaRelacijeNaLinijuCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.DodajStajalistaRelacijeNaLinijuCompleted(this, new DodajStajalistaRelacijeNaLinijuCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void DodajStajalistaRelacijeNaLinijuAsync(int idLinije, int idRelacije) {
            this.DodajStajalistaRelacijeNaLinijuAsync(idLinije, idRelacije, null);
        }
        
        public void DodajStajalistaRelacijeNaLinijuAsync(int idLinije, int idRelacije, object userState) {
            if ((this.onBeginDodajStajalistaRelacijeNaLinijuDelegate == null)) {
                this.onBeginDodajStajalistaRelacijeNaLinijuDelegate = new BeginOperationDelegate(this.OnBeginDodajStajalistaRelacijeNaLiniju);
            }
            if ((this.onEndDodajStajalistaRelacijeNaLinijuDelegate == null)) {
                this.onEndDodajStajalistaRelacijeNaLinijuDelegate = new EndOperationDelegate(this.OnEndDodajStajalistaRelacijeNaLiniju);
            }
            if ((this.onDodajStajalistaRelacijeNaLinijuCompletedDelegate == null)) {
                this.onDodajStajalistaRelacijeNaLinijuCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnDodajStajalistaRelacijeNaLinijuCompleted);
            }
            base.InvokeAsync(this.onBeginDodajStajalistaRelacijeNaLinijuDelegate, new object[] {
                        idLinije,
                        idRelacije}, this.onEndDodajStajalistaRelacijeNaLinijuDelegate, this.onDodajStajalistaRelacijeNaLinijuCompletedDelegate, userState);
        }
        
        public RVMS.Model.DTO.LinijaSaKandidatimaDTO SkloniStajalisteSaLinije(int idLinije, int idStajalista) {
            return base.Channel.SkloniStajalisteSaLinije(idLinije, idStajalista);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult BeginSkloniStajalisteSaLinije(int idLinije, int idStajalista, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginSkloniStajalisteSaLinije(idLinije, idStajalista, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public RVMS.Model.DTO.LinijaSaKandidatimaDTO EndSkloniStajalisteSaLinije(System.IAsyncResult result) {
            return base.Channel.EndSkloniStajalisteSaLinije(result);
        }
        
        private System.IAsyncResult OnBeginSkloniStajalisteSaLinije(object[] inValues, System.AsyncCallback callback, object asyncState) {
            int idLinije = ((int)(inValues[0]));
            int idStajalista = ((int)(inValues[1]));
            return this.BeginSkloniStajalisteSaLinije(idLinije, idStajalista, callback, asyncState);
        }
        
        private object[] OnEndSkloniStajalisteSaLinije(System.IAsyncResult result) {
            RVMS.Model.DTO.LinijaSaKandidatimaDTO retVal = this.EndSkloniStajalisteSaLinije(result);
            return new object[] {
                    retVal};
        }
        
        private void OnSkloniStajalisteSaLinijeCompleted(object state) {
            if ((this.SkloniStajalisteSaLinijeCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.SkloniStajalisteSaLinijeCompleted(this, new SkloniStajalisteSaLinijeCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void SkloniStajalisteSaLinijeAsync(int idLinije, int idStajalista) {
            this.SkloniStajalisteSaLinijeAsync(idLinije, idStajalista, null);
        }
        
        public void SkloniStajalisteSaLinijeAsync(int idLinije, int idStajalista, object userState) {
            if ((this.onBeginSkloniStajalisteSaLinijeDelegate == null)) {
                this.onBeginSkloniStajalisteSaLinijeDelegate = new BeginOperationDelegate(this.OnBeginSkloniStajalisteSaLinije);
            }
            if ((this.onEndSkloniStajalisteSaLinijeDelegate == null)) {
                this.onEndSkloniStajalisteSaLinijeDelegate = new EndOperationDelegate(this.OnEndSkloniStajalisteSaLinije);
            }
            if ((this.onSkloniStajalisteSaLinijeCompletedDelegate == null)) {
                this.onSkloniStajalisteSaLinijeCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnSkloniStajalisteSaLinijeCompleted);
            }
            base.InvokeAsync(this.onBeginSkloniStajalisteSaLinijeDelegate, new object[] {
                        idLinije,
                        idStajalista}, this.onEndSkloniStajalisteSaLinijeDelegate, this.onSkloniStajalisteSaLinijeCompletedDelegate, userState);
        }
    }
}

<template>
    <modal :show="show">
        <modal show="show" v-if=" isValid('CanAddColor') || isValid('CanEditColor') ">
            <div class="modal-content">
                <div class="modal-header">
                        <h6 class="modal-title m-0" id="myModalLabel" v-if="isFlushData"> {{ $t('TheSupervisorLogin.SuperAdminLogin') }}</h6>
                        <h6 class="modal-title m-0" id="myModalLabel" v-else> {{ $t('TheSupervisorLogin.SupervisorLogin') }}</h6>
                       <button type="button" class="btn-close" v-on:click="close()"></button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="form-group has-label col-sm-12 ">
                            <label class="text  font-weight-bolder"> {{ $t('TheSupervisorLogin.User') }}:<span class="text-danger"> *</span> </label>
                            <input class="form-control" v-model="v$.login.email.$model" type="text" />
                        </div>
                        <div class="form-group has-label col-sm-12 ">
                            <label class="text  font-weight-bolder"> {{ $t('TheSupervisorLogin.Password') }}:<span class="text-danger"> *</span> </label>
                            <input class="form-control" v-model="v$.login.password.$model" type="password" />
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SupervisorLogin()"> {{ $t('TheSupervisorLogin.Login1') }}</button>
                    <button type="button" class="btn btn-soft-secondary btn-sm" v-on:click="close()">{{ $t('TheSupervisorLogin.Cancel') }}</button>
                </div>
              
            </div>



        </modal>


    </modal>
    
</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
    
   

    import { required } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
    export default {
        mixins: [clickMixin],
        
        props: ['show', 'isFlushData','isReset'],
          setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {
                login: {
                    email: '',
                    password: '',
                    isFlushData:false,
                },

            }
        },
        validations() {
          return{
              login: {
                email:
                {
                    required
                },
                password:
                {
                    required
                }
            }
          }
        },
        methods: {
           
            close: function (x) {
                if (x) {
                    this.$emit('close', true);

                }
                else {
                    this.$emit('close', false);

                }
            },
            SupervisorLogin: function () {
                
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                if(this.isFlushData){
                    this.login.isFlushData = true
                }
               
                if (root.isReset) {
                    this.$swal({
                        title: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Are you sure?' : 'هل أنت متأكد؟', 
                        text: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'You will not be able to recover this!' : 'لن تتمكن من استرداد هذا!', 
                        type: "warning",
                        showCancelButton: true,
                        confirmButtonColor: "#DD6B55",
                        confirmButtonText: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Yes, delete it!' : 'نعم ، احذفها!', 
                        closeOnConfirm: false,
                        closeOnCancel: false
                    }).then(function (result) {
                        
                        if (result.isConfirmed) {
                            root.$https.post('/Product/SupervisorLogin', root.login, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                                
                                if (response.data != null) {
                                    if (response.data === 'Not Valid Credential') {
                                        root.$swal({
                                            title: 'error',
                                            text: 'Please enter valid Credential',
                                            type: 'error',
                                            icon: 'error',
                                            showConfirmButton: false,
                                            timer: 2500,
                                            timerProgressBar: true,
                                        });
                                        root.close(false);
                                    }
                                    else if (response.data === 'No Permission') {
                                        root.$swal({
                                            title: 'error',
                                            text: 'You do not have permission. Please enter Supervisor credential',
                                            type: 'error',
                                            icon: 'error',
                                            showConfirmButton: false,
                                            timer: 2500,
                                            timerProgressBar: true,
                                        });
                                        root.close(false);

                                    }
                                    else {
                                        if (!root.isFlushData) {
                                            root.$swal({
                                                title: 'success',
                                                text: "Login Successfully",
                                                type: 'success',
                                                icon: 'success',
                                                showConfirmButton: false,
                                                timer: 1500,
                                                timerProgressBar: true,
                                            });
                                            
                                            localStorage.setItem('IsSupervisor', true);
                                        }
                                        localStorage.setItem('SupervisorId', response.data);
                                        localStorage.setItem('SupervisorUserName', root.login.email);
                                        localStorage.setItem('SupervisorPassword', root.login.password);
                                        root.close(true);
                                    }

                                }

                            }).catch(error => {
                                console.log(error)
                                root.$swal.fire(
                                    {
                                        icon: 'error',
                                        title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Something Went Wrong!' : 'هل هناك خطب ما!',
                                        text: error.response.data,
                                        showConfirmButton: false,
                                        timer: 5000,
                                        timerProgressBar: true,
                                    });

                            });
                        }
                    });
                }
                else {
                    root.$https.post('/Product/SupervisorLogin', this.login, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                        if (response.data != null) {
                            if (response.data === 'Not Valid Credential') {
                                root.$swal({
                                    title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                                    text: 'Please enter valid Credential',
                                    type: 'error',
                                    icon: 'error',
                                    showConfirmButton: false,
                                    timer: 2500,
                                    timerProgressBar: true,
                                });
                                //root.close(false);
                            }
                            else if (response.data === 'No Permission') {
                                root.$swal({
                                    title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                                    text: 'You do not have permission. Please enter Supervisor credential',
                                    type: 'error',
                                    icon: 'error',
                                    showConfirmButton: false,
                                    timer: 2500,
                                    timerProgressBar: true,
                                });
                                //root.close(false);

                            }
                            else {
                                if (!root.isFlushData) {
                                    root.$swal({
                                        title: 'success',
                                        text: "Login Successfully",
                                        type: 'success',
                                        icon: 'success',
                                        showConfirmButton: false,
                                        timer: 1500,
                                        timerProgressBar: true,
                                    });
                                    
                                    localStorage.setItem('IsSupervisor', true);
                                }
                                localStorage.setItem('SupervisorId', response.data);
                                localStorage.setItem('SupervisorUserName', root.login.email);
                                localStorage.setItem('SupervisorPassword', root.login.password);
                                root.close(true);
                            }

                        }

                    }).catch(error => {
                        console.log(error)
                        root.$swal.fire(
                            {
                                icon: 'error',
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Something Went Wrong!' : 'هل هناك خطب ما!',
                                text: error.response.data,
                                showConfirmButton: false,
                                timer: 5000,
                                timerProgressBar: true,
                            });

                    });
                }
                

            },
        },
        mounted: function () {
            
        }
    }
</script>

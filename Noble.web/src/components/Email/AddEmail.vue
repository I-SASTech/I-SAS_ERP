<template>
    <modal show="show" v-if="isValid('BasicMail') || isValid('StandardMail')">
        <div class="modal-content">
            <div class="modal-header">
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-if="type=='Edit'"> {{ $t('Update Email Configuration') }}</h6>
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-else>{{ $t('Add Email Configuration') }}</h6>
                <button type="button" class="btn-close" v-on:click="close()"></button>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="mb-2">
                        <h6><b>To fill below information please first see helping material for all fields.</b></h6>
                        <a class="btn btn-primary" data-bs-toggle="offcanvas" href="#offcanvasExample" role="button" aria-controls="offcanvasExample">
                            Helping Material
                        </a>
                    </div>
                    <div class="form-group has-label col-sm-12 ">
                        <label class="text  font-weight-bolder">  {{($t('Host'))  }}:<span class="text-danger"> *</span> </label>
                        <multiselect :options="options" v-model="emailConfiguration.server"  :show-labels="false" placeholder="Select Type">
                        </multiselect>
                    </div>
                    <div class="form-group has-label col-sm-12 " v-if="isValid('BasicMail')">
                        <label class="text  font-weight-bolder"> {{($t('Email'))  }}:<span class="text-danger"> *</span><span class="text-warning ms-1" v-if="isValid('BasicMail')">You can only use your gmail server.</span> </label>
                        <div class="d-flex">
                            <input class="form-control" v-model="emailConfiguration.email"  @blur="checkEmail" :placeholder="$t('Email Address')" />
                        </div>
                       
                    </div>
                    <div class="form-group has-label col-sm-12 " v-else>
                        <label class="text  font-weight-bolder"> {{($t('Email'))  }}:<span class="text-danger"> *</span></label>
                        <div class="d-flex">
                            <input class="form-control" v-model="emailConfiguration.email" :placeholder="$t('Email Address')"/>
                        </div>
                       
                    </div>

                    <div class="form-group has-label col-sm-12 ">
                        <label class="text  font-weight-bolder">  {{($t('Password'))  }}:<span class="text-danger"> *</span> </label>
                        <input class="form-control " v-model="emailConfiguration.password" :placeholder="$t('Password')" />
                    </div>

                    <div class="form-group has-label col-sm-12 ">
                        <label class="text  font-weight-bolder">  {{($t('Smtp Server'))  }}:<span class="text-danger"> *</span> </label>
                        <input class="form-control " v-model="emailConfiguration.smtpServer" type="text" :placeholder="$t('SMTP Server')"  />
                    </div>

                    <div class="form-group has-label col-sm-12 ">
                        <label class="text  font-weight-bolder">  {{($t('Port'))  }}:<span class="text-danger"> *</span> </label>
                        <input class="form-control " type="number" v-model="emailConfiguration.port" :placeholder="$t('Port #')" />
                    </div>
                    
                </div>
                <div class="offcanvas offcanvas-end" tabindex="-1" id="offcanvasExample" aria-labelledby="offcanvasExampleLabel" style="width:600px">
                    <div class="offcanvas-header">
                        <h5 class="offcanvas-title" id="offcanvasExampleLabel">Helping Material</h5>
                        <button type="button" class="btn-close text-reset" data-bs-dismiss="offcanvas" aria-label="Close"></button>
                    </div>
                    <div class="offcanvas-body">
                        <div id="ForOutlook" v-if="isValid('StandardMail')">
                            <h4><b>Outlook</b></h4>
                            <p><b>Email: </b> example@outlook.com</p>
                            <p><b>Password: </b> Your Outlook account Password</p>
                            <p><b>Smtp Server: </b> For Smtp server just copy quoted words "smtp-mail.outlook.com."</p>
                            <p><b>Smtp Server: </b> If above is not working then use this "smtp.live.com."</p>
                            <p><b>Port: </b> For Port just copy quoted words "587"</p>
                        </div>
                        <div id="HotMail" v-if="isValid('StandardMail')"> 
                            <h4><b>HotMail</b></h4>
                            <p><b>Email: </b> example@hotmail.com</p>
                            <p><b>Password: </b> Your HotMail account Password</p>
                            <p><b>Smtp Server: </b> For Smtp server just copy quoted words "smtp-mail.outlook.com"</p>
                            <p><b>Smtp Server: </b> If above is not working then use this "smtp.live.com."</p>
                            <p><b>Port: </b> For Port just copy quoted words "587"</p>
                        </div>
                        <div id="MSN" v-if="isValid('StandardMail')">
                            <h4><b>MSN</b></h4>
                            <p><b>Email: </b> example@msn.com</p>
                            <p><b>Password: </b> Your MSN account Password</p>
                            <p><b>Smtp Server: </b> For Smtp server just copy quoted words "smtp-mail.outlook.com"</p>
                            <p><b>Smtp Server: </b> If above is not working then use this "smtp.live.com."</p>
                            <p><b>Port: </b> For Port just copy quoted words "587"</p>
                        </div>
                        <div id="Live" v-if="isValid('StandardMail')">
                            <h4><b>Live</b></h4>
                            <p><b>Email: </b> example@live.com</p>
                            <p><b>Password: </b> Your Live account Password</p>
                            <p><b>Smtp Server: </b> For Smtp server just copy quoted words "smtp-mail.outlook.com"</p>
                            <p><b>Smtp Server: </b> If above is not working then use this "smtp.live.com."</p>
                            <p><b>Port: </b> For Port just copy quoted words "587"</p>
                        </div>
                        <div id="Yahoo" v-if="isValid('StandardMail')">
                            <h4><b>Yahoo</b></h4>
                            <p><b>Email: </b> example@yahoo.com</p>
                            <p><b>Password: </b> Your Yahoo account Password</p>
                            <p><b>Smtp Server: </b> For Smtp server just copy quoted words "smtp.mail.yahoo.com"</p>
                            <p><b>Port: </b> For Port just copy quoted words "465"</p>
                        </div>
                        <div id="Business" v-if="isValid('StandardMail')">
                            <h4><b>Business</b></h4>
                            <p><b>Email: </b> no-reply@companyname.com</p>
                            <p><b>Password: </b> Your Company account Password</p>
                            <p><b>Smtp Server: </b> For Smtp server just paste your company server name like "mail.companyname.com"</p>
                            <p><b>Port: </b> Just copy your server port and paste it.</p>
                        </div>
                        <div id="Gmail" v-if="isValid('BasicMail') || isValid('StandardMail')">
                            <h4><b>Gmail</b></h4>
                            <p><b>Email: </b> example@gmail.com</p>
                            <p><b>Password: </b> For this you need to follow below steps</p>
                            <p><b>Smtp Server: </b> For Smtp server just paste your company server name like "smtp.gmail.com"</p>
                            <p><b>Port: </b> For Port just copy quoted words "587"</p>
                            <h6 class="text-center"><b>There will more setting that need to do with your gmail account.</b></h6>
                            <p><b>1: </b> Go to your gmail Account</p>
                            <img src="1.png"  style="width:100%;" />
                            <p><b>2: </b> Go to <b>Setting</b> and then click on <b>See All setting</b></p>
                            <img src="2.png"  style="width:100%;" />
                            <p><b>3: </b> Click on <b>Forwarding and POP/IMAP</b> and then <b>Enable IMAP</b> in <b>IMAP access:</b></p>
                            <img src="3.png"  style="width:100%;" />
                            <p><b>4: </b> Click on <b>Profile Icon</b> and then click on <b>Manage your Google Account</b></p>
                            <img src="4.png"  style="width:100%;" />
                            <p><b>5: </b> Click on <b>Security</b></p>
                            <img src="5.png"  style="width:100%;" />
                            <p><b>6: </b> In <b>How you sign in to Google</b> section click on <b>2-Step Verification</b> and then click on <b>Get started</b> after that give your google account password</p>
                            <img src="6.png"  style="width:100%;" />
                            <p><b>7: </b> After completing step 6 you will need to click on <b>CONTINUE</b></p>
                            <img src="7.png"  style="width:100%;" />
                            <p><b>8: </b> In this step you have few options like you provide your <b>Phone</b> 
                                number then receive <b>Text message</b> or <b>Phone call</b> 
                                from google or use <b>Use another backup option</b>. 
                                After completing this you need click on <b>Next or Continue</b> 
                                and then <b>Turn on</b>
                                <br />
                                <span class="text-danger">If you are using <b>Use another backup option</b> you need to download that options and save these option on safe side of your computer or mobile</span>
                            </p>
                            <img src="8.png"  style="width:100%;" />
                            <p><b>9: </b> Repeat Step 4 and 5</p>
                            <p><b>10: </b> In <b>How you sign in to Google</b> section click on <b>2-Step Verification</b> and then fill your password and <b>Next or Continue</b></p>
                            <p><b>11: </b> it will take <b>2-Step Verification</b> screen scroll down to the end until you see <b>App passwords</b> then click on <b>Forward Icon</b></p>
                            <img src="9.png"  style="width:100%;" />
                            <p><b>12: </b> In <b>App name</b> field type "Smtp" and then click on <b>Create</b> it will open the popup model <b>Generated app password</b> from here you need copy the password click on <b>Done</b>. You will need to save this password on the safe side of your computer or mobile.</p>
                            <img src="10.png"  style="width:100%;" />
                            <p><b>13: </b> Put this password in <b>Password</b> field.</p>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveColor" v-bind:disabled="v$.newEmailConfiguration.$invalid" v-if="type!='Edit'">{{ $t('AddColors.btnSave') }}</button>
                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveColor" v-bind:disabled="v$.newEmailConfiguration.$invalid" v-if="type=='Edit'">{{ $t('AddColors.btnUpdate') }}</button>
                <button type="button" class="btn btn-soft-secondary btn-sm" v-on:click="close()">{{ $t('AddColors.btnClear') }}</button>
            </div>
            <loading v-model:active="loading" :can-cancel="false" :is-full-page="true"></loading>
           
        </div>
       


    </modal>
    <acessdenied v-else :model=true></acessdenied>
</template>


<script>
    import clickMixin from '@/Mixins/clickMixin'
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    import { required } from '@vuelidate/validators'; 
    import useVuelidate from '@vuelidate/core'
    import Multiselect from 'vue-multiselect'
    
    export default {
        props: ['show', 'newEmailConfiguration', 'type'],
        mixins: [clickMixin],
        components: {
            Multiselect,
            Loading
        },
        setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {
                options:[],
                userName:'',
                server:'',

                arabic: '',
                english: '',
                render: 0,
                loading: false,
                emailConfiguration: this.newEmailConfiguration,
                show1: false,
            }
        },
        validations() {
            return{
                newEmailConfiguration: {
                    email: { required },
                    password: { required },
                    smtpServer: { required },
                    port: { required },
                    server: { required },
                }
            }
        },
        methods: {
            IsSave1: function () {

                this.show1 = false;
            },
            openmodel: function () {
                this.show1 = !this.show1;
            },
            checkEmail() {
                if(this.emailConfiguration.email == '')
                {
                    return;
                }
                else if (!this.emailConfiguration.email.includes('@gmail.com')) {
                    this.error = 'Error: ';
                    this.emailConfiguration.email = '';
                    this.$swal({
                        title: 'Invalid Mail Account',
                        text: 'Email must be a Gmail address.',
                        type: 'error',
                        icon: 'error',
                        showConfirmButton: false,
                        timer: 1500,
                        timerProgressBar: true,
                    });
                }
            },
            close: function () {
                this.$emit('close');
            },
            
            SaveColor: function () {
                var root = this;
                this.loading = true;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https.post('/Product/SaveEmailConfiguration', this.emailConfiguration, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data.isSuccess == true) {

                            if (root.type != "Edit") {

                                root.$swal({
                                    title: root.$t('AddColors.SavedSuccessfully'),
                                    text: root.$t('AddColors.Saved'),
                                    type: 'success',
                                    icon: 'success',
                                    showConfirmButton: false,
                                    timer: 1500,
                                    timerProgressBar: true,
                                });

                                root.close();
                            }
                            else {

                                root.$swal({
                                    title: root.$t('AddColors.SavedSuccessfully'),
                                    text: root.$t('AddColors.UpdateSucessfully'),
                                    type: 'success',
                                    icon: 'success',
                                    showConfirmButton: false,
                                    timer: 1500,
                                    timerProgressBar: true,
                                });
                                root.close();
                            }

                        }
                        else
                        {
                            root.$swal({
                                title: 'Error',
                                text: response.data.isAddUpdate,
                                type: 'error',
                                icon: 'error',
                                showConfirmButton: false,
                                timer: 1500,
                                timerProgressBar: true,
                            });
                        }
                    }).catch(error => {
                        root.$swal.fire(
                            {
                                icon: 'error',
                                title: this.$t('AddColors.SomethingWrong'),
                                text: error.response.data,

                                showConfirmButton: false,
                                timer: 5000,
                                timerProgressBar: true,
                            });

                        root.loading = false
                    })
                    .finally(() => root.loading = false)
            }
        },
        created: function()
        {
            if(this.isValid('BasicMail'))
            {
                this.options = ['Gmail'];
                this.emailConfiguration.isActive = true;
            }
            else if(this.isValid('StandardMail'))
            {
                this.options = ['Gmail', 'Outlook','HotMail', 'Msn', 'Live', 'Yahoo', 'Business'];
            }

        },
        mounted: function () {
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
        }
    }
</script>

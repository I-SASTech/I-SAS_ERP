<template>
    <div>
        <modal :show="show" :modalLarge="true" v-if="show && isShow">

            <div style="margin-bottom:0px" class="card">
                <div class="card-body">
                    <div class="col-lg-12">
                        <div class="tab-content mt-2" id="nav-tabContent">
                            <div class="modal-header">

                                <h5 class="modal-title" id="myModalLabel"> Ftp Account Details </h5>

                            </div>
                            <div class="row ">

                                <div class="col-sm-6" v-bind:class="{'has-danger' : v$.ftpDetail.host.$error}">
                                    <label>Ftp Server :<span class="text-danger">*</span></label>
                                    <input class="form-control" v-model="v$.ftpDetail.host.$model" type="text" />
                                    <span v-if="v$.ftpDetail.host.$error" class="error">
                                        <span v-if="!v$.ftpDetail.host.required"> Host is required</span>
                                    </span>
                                </div>
                                <div class="form-group has-label col-sm-6 ">
                                    <label class="text  font-weight-bolder"> Ftp Port </label>
                                    <input class="form-control" v-model="ftpDetail.port" type="text" />

                                </div>
                                <div class="col-sm-6">
                                    <label>Ftp UserName</label>
                                    <input class="form-control" v-model="ftpDetail.username" type="text" />
                                </div>
                                <div class="form-group has-label col-sm-6 ">
                                    <label class="text  font-weight-bolder"> Ftp Password </label>
                                    <input class="form-control" v-model="ftpDetail.password" type="text" />
                                    
                                </div>
                            </div>
                            <div class="modal-footer justify-content-right">
                                <button type="button" class="btn btn-primary " v-bind:disabled="v$.ftpDetail.$invalid" v-on:click="ApplyWhiteLabelling"> Apply WhiteLabeling </button>
                                <button type="button" class="btn btn-danger   " v-on:click="close()">Close</button>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </modal>
        <loading v-model:active="loading"
                 :can-cancel="true"
                 :is-full-page="true"></loading>
    </div>
</template>
<script>
    import { required } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
    import axios from 'axios'
     import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    export default {
        props: ['show', 'newftpdetail'],
        components: {
            Loading
        },
         setup() {
            return { v$: useVuelidate() }
        },

        data: function () {
            return {

                loading: false,
                isShow: false,
                rander: 0,
                whiteLabelling: '',
                ftpDetail: this.newftpdetail,
            }
        },
        validations() {
         return{
               ftpDetail: {
                host: {
                    required,
                },
            }
         }
        },
        methods: {

            close: function () {
                this.$emit('close', true);
            },

            SaveLicense: function () {
                this.loading = true;
                var root = this;
                root.$https.post('/NoblePermission/SaveFtpAccountDetail', this.ftpDetail).then(function (response) {
                    if (response.data.isSuccess) {
                        root.$swal({
                            icon: 'success',
                            title: 'Saved Successfully!',
                            showConfirmButton: false,
                            timer: 1500,
                            timerProgressBar: true,
                        });
                        root.loading = false
                    }

                });

            },


            GetWhitelabellingData: function () {
                var root = this;
                this.isShow = false;
                axios.get(root.$PermissionIp + '/NoblePermission/GetWhiteLabelingData').then(function (response) {
                    if (response.data != null) {
                        root.whiteLabelling = response.data.result;
                        root.isShow = true;
                    }

                }).catch(error => {
                    console.log(error)
                    root.close();
                    root.$swal.fire(
                        {
                            icon: 'error',
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Something Went Wrong!' : 'هل هناك خطب ما!',
                            text: 'Please Contact to support to update license',

                            showConfirmButton: false,
                            timer: 5000,
                            timerProgressBar: true,
                        });
                    root.loading = false

                });
            },
            ApplyWhiteLabelling: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.loading = true;
                this.whiteLabelling.host = this.ftpDetail.host;
                this.whiteLabelling.port = this.ftpDetail.port;
                this.whiteLabelling.userName = this.ftpDetail.username;
                this.whiteLabelling.password = this.ftpDetail.password;
                this.whiteLabelling.isWhiteLabbeling = this.ftpDetail.isWhiteLabbeling;
                this.$https.post('/Company/ApplyWhiteLabelling', this.whiteLabelling, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data.isSuccess == true) {

                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved!' : '!تم الحفظ',
                                text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Update Successfully!' : 'تم التحديث بنجاح',
                                type: 'success',
                                icon: 'success',
                                showConfirmButton: false,
                                timer: 1500,
                                timerProgressBar: true,
                            });
                            root.close();
                        }
                        root.loading = false
                    }).catch(error => {
                        console.log(error)
                        root.$swal.fire(
                            {
                                icon: 'error',
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Something Went Wrong!' : 'هل هناك خطب ما!',
                                text: error,

                                showConfirmButton: false,
                                timer: 5000,
                                timerProgressBar: true,
                            });

                        root.loading = false
                    });
            },
        },


        created: function () {
            
            this.GetWhitelabellingData()
        },
    }
</script>
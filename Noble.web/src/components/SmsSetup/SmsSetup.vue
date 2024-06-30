<template>
    <div class="row">
        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">GSM SMS Setup</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                    <li class="breadcrumb-item active">GSM SMS Setup</li>
                                </ol>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="card col-md-6">
                <div class="card-body">
                    <div class="row ">
                        <div class="row form-group">
                            <label class="col-form-label col-lg-4">
                                <span class="tooltip-container text-dashed-underline "> Com Port</span>
                            </label>
                            <div class="inline-fields col-lg-8">
                                <multiselect v-model="smsSetup.comPort" :options="portOptions" :show-labels="false"
                                    v-bind:placeholder="$t('SelectOption')">
                                </multiselect>

                            </div>
                        </div>
                        <div class="row form-group">
                            <label class="col-form-label col-lg-4">
                                <span class="tooltip-container text-dashed-underline "> Default Message</span>
                            </label>
                            <div class="inline-fields col-lg-8">
                                <textarea class="form-control " rows="12" v-model="smsSetup.defaultMessage" />
                            </div>
                        </div>
                       


                        <div class="col-sm-12 arabicLanguage">
                            <button type="button" class="btn btn-outline-primary me-2" v-on:click="SaveSetting"><i
                                    class="far fa-save"></i> Save</button>

                        </div>
                    </div>
                </div>

            </div>
        </div>
        <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
    </div>
</template>
<script>

import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
import Multiselect from 'vue-multiselect';
export default ({
    components: {
        Multiselect,
        Loading
    },

    data: function () {
        return {
            loading: false,
            smsSetup: {
                comPort: '',
                defaultMessage: '',
                branchId: '',

            },
            portOptions: []
        }
    },

    methods: {

        SaveSetting: function () {
            this.loading = true;
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            this.smsSetup.branchId = localStorage.getItem('BranchId');

            this.$https
                .post('/Sale/AddUpdateGsmSetup', this.smsSetup, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {

                    if (response.data != null) {
                        localStorage.setItem('Port', root.smsSetup.comPort);
                        localStorage.setItem('DefaultMessage', root.smsSetup.defaultMessage);

                        root.$swal({
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved!' : '!تم الحفظ',
                            text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved Successfully!' : '!حفظ بنجاح',
                            type: 'success',
                            icon: 'success',
                            showConfirmButton: false,
                            timer: 1500,
                            timerProgressBar: true,
                        });
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

                    root.loading = false
                })
                .finally(() => root.loading = false)
        },

        GetData: function () {
            this.loading = true;
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }

            this.$https
                .get('/Sale/GetGsmSetting?branchId=' + localStorage.getItem('BranchId'), { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {

                    if (response.data != null) {
                        root.smsSetup.comPort = response.data.result.comPort;
                        root.smsSetup.defaultMessage = response.data.result.defaultMessage;
                        root.portOptions = response.data.result.ports;
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

                    root.loading = false
                })
                .finally(() => root.loading = false)
        },

    },
    created: function () {
        this.GetData()

    },
    mounted: function () {

    }
})
</script>
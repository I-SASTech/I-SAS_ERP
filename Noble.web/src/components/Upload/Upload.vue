<template>
    <div class="row">

        <div class="col-lg-12">
            <div class="row">
                <div class="col-lg-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('ManageFiles.ManageFiles') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('ManageFiles.Home') }}</a></li>
                                    <li class="breadcrumb-item active">{{ $t('ManageFiles.ManageFiles') }}</li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">
                                <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);" class="btn btn-sm btn-outline-danger">
                                    {{ $t('Categories.Close') }}
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6">
                    <div class="card ">
                        <div class="col-md-12">
                            <h4 class="card-title text-center my-3 "> {{ $t('ManageFiles.ManageFiles') }}</h4>
                            <div class="col-md-12 pb-3">
                                <div class="mb-3 text-center">
                                    {{ $t('ManageFiles.Description') }}
                                </div>
                                <div class="mb-3 text-center text-danger" v-if="loading">
                                    {{ $t('ManageFiles.OpDescription') }}
                                </div>

                                <div class="form-group text-center">
                                    <button class="btn btn-primary  " @click="UploadFiles" :disabled="loading"><i class="fas fa-cloud-upload-alt"></i> {{ $t('ManageFiles.UploadFiles') }}</button>
                                    <button class="btn btn-primary mx-1 " @click="DownloadFiles" :disabled="loading"><i class="fas fa-download"></i> {{ $t('ManageFiles.DownloadFiles') }}</button>
                                </div>
                            </div>
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
    export default {
        name: "Upload",
        components: {
            Loading
        },
        data: function () {
            return {
                loading: false,
                opration: '',
                path: '',
            }
        },
        methods: {
              UploadFiles: function () {
                var root = this;

                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                this.loading = true;
                this.opration = "Uploading start."
                this.$https
                    .get('/System/UploadFiles', { headers: { "Authorization": `Bearer ${token}` } })
                     .then((res) =>  { 
                         if(res.data.statusCode == 200){
                              root.$swal({
                                title: "Success!",
                                text: "Files upload successfully",
                                type: 'success',
                                confirmButtonClass: "btn btn-Success",
                                buttonStyling: false,
                                icon: 'success'

                            });
                         } else {
                             console.log(res)
                              root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                                text: res,
                                type: 'error',
                                confirmButtonClass: "btn btn-danger",
                                buttonStyling: false,
                                icon: 'error'
                            });
                         }
                        
                         root.loading = false;
                         this.opration = ""
                         }, 
                    () =>  {root.isSync = false;});
            },

             DownloadFiles: function () {
                var root = this;

                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.loading = true;
                this.opration = "Downloading start."

                this.$https
                    .get('/System/DownloadFiles', { headers: { "Authorization": `Bearer ${token}` } })
                     .then((res) =>  {
                          if(res.data.statusCode == 200){
                             root.$swal({
                                title: "Success!",
                                text: "Files download successfully",
                                type: 'success',
                                confirmButtonClass: "btn btn-Success",
                                buttonStyling: false,
                                icon: 'success'

                            });
                         }else{
                              root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                                text: "Something went wrong. Please try again or contact to support.",
                                type: 'error',
                                confirmButtonClass: "btn btn-danger",
                                buttonStyling: false,
                                icon: 'error'
                            });
                         }
                            
                         root.loading = false;
                         this.opration = ""
                         }, () =>  {root.isSync = false;});
            }
        },
        created: function () {
        },
        mounted: function () {
        }
    }
</script>
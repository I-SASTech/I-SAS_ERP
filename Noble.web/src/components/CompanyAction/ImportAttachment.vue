<template>
    <modal :show="show" v-if=" isValid('CanUploadAttachment') || isValid('CanUploadExpenseAttachment') || isValid('CanUploadExpenseBillAttachment') ">
        <div style="margin-bottom:0px" class="card" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
            <div class="card-body">
                <div class="col-lg-12">
                    <div class="tab-content" id="nav-tabContent">
                        
                        <div class="card-body">
                            <div class="row mb-2">
                                <div class="col-lg-12 ml-auto mr-auto">
                                    <label>Description</label>
                                    <textarea class="form-control" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-model="description" type="text" />
                                </div>
                            </div>
                            <div class="row mb-2" :key="render">
                                <div class="col-lg-12 ml-auto mr-auto">
                                    <label>File Path</label>
                                    <!--<b-form-file v-model="file1"
                                                 id="uplaodfile"
                                                 ref="imgupload1"
                                                 :no-drop="true"
                                                 :state="Boolean(file1)"
                                                 v-bind:placeholder="$t('ImportAttachment.ChooseFile')"></b-form-file>-->
                                </div>
                            </div>
                        </div>
                            <div class="modal-footer row" >
                                <div class="col-lg-12 mt-3 " v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-right' : 'text-left'">
                                    <button class="btn btn-primary  "
                                            v-bind:class="{'disabled': (file1 == null ||  loading==true) }"
                                            @click="uploadFile">
                                        <i class="nc-icon nc-cloud-upload-94"></i> {{ $t('ImportAttachment.Upload') }}
                                    </button>
                                    <button class="btn btn-danger   mr-2"
                                            v-on:click="close">
                                        {{ $t('ImportAttachment.Cancel') }}
                                    </button>
                                </div>
                            </div>
                        

                    </div>
                </div>
            </div>
             <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
        </div>
    </modal>
    <acessdenied v-else :model=true></acessdenied>
</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
    /*import { BFormFile } from 'bootstrap-vue';*/
       import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    import moment from "moment";
    export default {
        components: {
        //    BFormFile,
        Loading
        },
        props: ['show', 'purchase', 'document'],
        mixins: [clickMixin],
        data: function () {
            return {
                arabic: '',
                english: '',
                description: '',
                file1: null,
                render: 0,
                loading: false
            }
        },
        methods: {
            close: function () {
                this.$emit('close');
            },
            SaveProcess: function (id,path) {
                var root = this;
                this.loading = true;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                
                var date = moment().format("LLL");
                this.$https.get('/Purchase/SavePurchaseOrderAttachment?id=' + id + '&path=' + path + '&date=' + date + '&description=' + this.description, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data.isSuccess == true) {
                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved!' : '!تم الحفظ',
                                text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved Successfully!' : '!حفظ بنجاح',
                                type: 'success',
                                icon: 'success',
                                showConfirmButton: false,
                                timer: 1500,
                                timerProgressBar: true,
                            });
                            root.close();
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
            SaveBills: function (path) {
                
                this.loading = true;
                
                var date = moment().format("LLL");
                var expenseAttachment = {
                    path: '',
                    date: '',
                    description: ''
                };
                expenseAttachment.path = path;
                expenseAttachment.date = date;
                expenseAttachment.description = this.description;
                this.$emit('billAttachments', expenseAttachment)
              
                
            },
            SaveDailyExpense: function (path) {
                
                this.loading = true;
                
                var date = moment().format("LLL");
                var billAttachments = {
                    path: '',
                    date: '',
                    description: ''
                };
                billAttachments.path = path;
                billAttachments.date = date;
                billAttachments.description = this.description;
                this.$emit('billAttachments', billAttachments)
              
                
            },

            uploadFile() {
                
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                var file = this.$refs.imgupload1.files;

                var fileData = new FormData();
                for (var k = 0; k < file.length; k++) {
                    fileData.append("files", file[k]);
                }
                
                root.$https.post('/Company/UploadFilesAsync', fileData, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null) {  

                            if (root.document == 'Bills') {
                                root.SaveBills(response.data);                            

                            }
                            else if (root.document == 'DailyExpense') {
                                root.SaveDailyExpense(response.data);                            

                            }
                            else {
                                root.SaveProcess(root.purchase.id, response.data);                            

                            }
                        }
                    },
                        function () {
                            root.loading = false;
                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                                text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Something Went Wrong!' : 'هل هناك خطب ما!',
                                type: 'error',
                                confirmButtonClass: "btn btn-danger",
                                buttonsStyling: false
                            });
                        });
            },

        },
        mounted: function () {
            
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
        }
    }
</script>

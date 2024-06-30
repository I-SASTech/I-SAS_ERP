<template>
    <modal :modalLarge="true" :show="show">
        <div class="modal-content">
            <div class="modal-header">
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel">{{ $t('ImportAttachment.Attachment') }}
                    (<small class="text-muted">
                        {{ $t('AddSale.FileSize') }}
                    </small>)</h6>
                <button type="button" class="btn-close" v-on:click="close()"></button>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="table-responsive">
                        <table class="table mb-0">
                            <thead class="thead-light table-hover">
                                <tr>
                                    <th width="5%">#</th>
                                    <th width="5%">
                                        {{ $t('ImportAttachment.Date') }}
                                    </th>
                                    <th width="35%">
                                        {{ $t('ImportAttachment.Description') }}
                                    </th>
                                    <th width="30%">
                                        {{ $t('ImportAttachment.Document') }}
                                    </th>

                                    <th width="5%" class="text-center">
                                        {{ $t('ImportAttachment.View') }}
                                    </th>
                                    <th width="5%" class="text-center">
                                        {{ $t('ImportAttachment.Download') }}
                                    </th>
                                    <th width="5%" class="text-center">

                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr v-for="(item, index) in attachementList" :key="index">
                                    <td>{{ index + 1 }}</td>
                                    <td>
                                        {{ item.date }}
                                    </td>
                                    <td>
                                        <input class="form-control" v-model="item.description" type="text" />
                                    </td>
                                    <td>
                                        {{ item.fileName }}
                                    </td>
                                    <td class="text-center">
                                        <button @click="ViewAttachment(item.path)" title="View Item"
                                            class="btn btn-soft-primary btn-round btn-sm  btn-icon">
                                            <i class="fas fa-eye"></i>
                                        </button>
                                    </td>
                                    <td class="text-center">
                                        <button class="btn btn-soft-primary btn-round btn-sm  btn-icon"
                                            v-on:click="DownloadAttachment(item.path)">
                                            <i class="fas fa-download"></i>
                                        </button>
                                    </td>
                                    <td class="text-end">
                                        <button @click="RemoveItem(index)" title="Add Attachement"
                                            class="btn btn-sm btn-soft-danger btn-circle">
                                            <i class="dripicons-trash" aria-hidden="true"></i>

                                        </button>
                                    </td>
                                </tr>
                                <tr v-if="attachementList.length < 3">
                                    <td class="text-center"></td>
                                    <td>{{ date }}</td>
                                    <td>
                                        <input class="form-control" v-model="description" type="text" />
                                    </td>
                                    <td>
                                      
                                        <div class="input-group ">
                                            <input type="file" v-bind:key="rander" class="form-control"
                                                id="inputGroupFile01" @change="uploadFile()" ref="imgupload1"
                                                :state="Boolean(file1)"
                                                v-bind:placeholder="$t('ImportAttachment.ChooseFile')">
                                        </div>

                                    </td>
                                    <td class="text-center"></td>
                                    <td class="text-center"></td>
                                    <td class="text-center"></td>
                                    <td class="text-end">
                                        <button @click="AddAttachement()" v-bind:disabled="path == ''" title="Add Attachement"
                                            class="btn btn-sm btn-soft-purple btn-circle">
                                            <i class="dripicons-checkmark"></i>
                                        </button>

                                    </td>
                                </tr>
                            </tbody>
                        </table>

                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button class="btn btn-soft-primary btn-sm " v-bind:disabled="attachementList.length == 0"
                    @click="SaveAttachement">
                    <i class="fas fa-cloud-upload-alt"></i> {{ $t('ImportAttachment.Upload') }}
                </button>
                <button class="btn btn-soft-secondary btn-sm   mr-2" v-on:click="close">
                    {{ $t('ImportAttachment.Cancel') }}
                </button>
            </div>
            <attachment-view :documentpath="documentpath" :show="showView" v-if="showView" @close="CloseModel" />
             <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
        </div>
    </modal>
</template>
<script>
import clickMixin from '@/Mixins/clickMixin'
import moment from "moment";
 import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
/*    import { BFormFile } from 'bootstrap-vue';*/
export default {
     components: {
            Loading
        },
    props: ['show', 'documentid', 'attachmentList'],
    mixins: [clickMixin],
    data: function () {
        return {
            arabic: '',
            english: '',
            date: '',
            path: '',
            fileName: '',
            description: '',
            file1: null,
            render: 0,
            rander: 0,
            loading: false,
            showView: false,
            documentpath: '',
            attachementList: [],
            branchId: ''
        }
    },
    methods: {
        RemoveItem: function (index) {
            this.attachementList.splice(index, 1);
        },
        ViewAttachment: function (path) {
            if (path != '' && path != undefined && path != null) {
                this.documentpath = path;
                this.showView = true;
            }
        },
        DownloadAttachment(path) {

            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            var ext = path.split('.')[1];
            root.$https.get('/Contact/DownloadFile?filePath=' + path, { headers: { "Authorization": `Bearer ${token}` }, responseType: 'blob' })
                .then(function (response) {
                    const url = window.URL.createObjectURL(new Blob([response.data]));
                    const link = document.createElement('a');
                    link.href = url;
                    link.setAttribute('download', 'file.' + ext);
                    document.body.appendChild(link);
                    link.click();
                });
        },
        CloseModel: function () {
            this.showView = false;
        },
        AddAttachement: function () {

            this.date = moment().format('l');
            this.attachementList.push({ date: this.date, documentId: this.documentid, description: this.description, path: this.path, fileName: this.fileName });

            this.description = '';
            this.file1 = null;
            this.path = '';
            this.fileName = '';
            this.rander++;
        },
        close: function () {

            this.$emit("close", this.attachementList);
        },
        SaveAttachement() {

            if (this.documentid != undefined) {
                this.loading = true;
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                root.$https.post('/Company/SaveAttachment', this.attachementList, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data.isSuccess) {
                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved!' : '!تم الحفظ',
                                text: 'Saved Successfuly',
                                type: 'success',
                                confirmButtonClass: "btn btn-success",
                                buttonStyling: false,
                                icon: 'success',
                                timer: 1500,
                                timerProgressBar: true
                            });
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
            }
            else {
                this.$swal({
                    title: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Saved!' : '!تم الحفظ',
                    text: 'Saved Successfuly',
                    type: 'success',
                    confirmButtonClass: "btn btn-success",
                    buttonStyling: false,
                    icon: 'success',
                    timer: 1500,
                    timerProgressBar: true
                });
            }


        },
        uploadFile() {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            var files = this.$refs.imgupload1.files[0];
            if (files.size > 1024 * 1024) {
                this.$refs.imgupload1 = "";
                this.rander++;
                root.$swal({
                    title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                    text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'File is to Big greater than 1 MB!' : 'حجم الملف أكبر من 1 ميغا بايت!',
                    type: 'error',
                    confirmButtonClass: "btn btn-danger",
                    buttonsStyling: false
                });
                return;
            }
            var file = this.$refs.imgupload1.files;

            var fileData = new FormData();
            for (var k = 0; k < file.length; k++) {
                fileData.append("files", file[k]);
                root.fileName = file[k].name;
            }

            root.$https.post('/Company/UploadFilesAsync?branchId=' + this.branchId,  fileData, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {
                    if (response.data != null) {
                        root.path = response.data;

                    }
                },
                    function () {
                        root.loading = false;

                        root.$swal({
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                            text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Something Went Wrong!' : 'هل هناك خطب ما!',
                            type: 'error',
                            confirmButtonClass: "btn btn-danger IndexToSawal",
                            buttonsStyling: false
                        });
                    });
        },
        GetAttachmentList() {

            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }

            root.$https.get('/Company/AttachmentList?id=' + this.documentid + '&prop=' + this.document, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {
                    if (response.data != null) {
                        response.data.forEach(function (x) {
                            root.attachementList.push({ date: moment(x.date).format('l'), documentId: x.documentId, description: x.description, path: x.path, fileName: x.fileName });
                        });
                    }

                });
        },
    },

    created: function () {

        var root = this;
        if (this.documentid != undefined) {
            this.GetAttachmentList();
        }
        else {
            this.attachmentList.forEach(function (x) {
                root.attachementList.push({ date: moment(x.date).format('l'), documentId: x.documentId, description: x.description, path: x.path, fileName: x.fileName });
            });
        }
    },

    mounted: function () {

        this.english = localStorage.getItem('English');
        this.arabic = localStorage.getItem('Arabic');
        this.branchId = localStorage.getItem('BranchId');
        this.date = moment().format('l');
    }
}
</script>
<style  scoped>
.IndexToSawal {
    z-index: 99999 !important;
}

.swal2-container {
    z-index: 99999 !important;
}

.swal-container {
    z-index: 99999 !important;
}

:host ::ng-deep .swal2-container {
    z-index: 300000 !important;
}
</style>

<template>
    <modal :show="show">

        <div style="margin-bottom:0px" class="card">
            <div class="card-body">
                <div class="col-lg-12">
                    <div class="tab-content" id="nav-tabContent">
                        <div class="modal-header">

                            <h5 class="modal-title DayHeading" id="myModalLabel"> {{ $t('EmailCompose.ReceivedMessage') }}</h5>

                        </div>
                        <div class="">
                            <div class="card-body">
                                <div class="row ">
                                    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                        <label>{{ $t('EmailCompose.From') }}: <span class="text-danger"> *</span></label>
                                        <multiselect v-model="emailCompoese.EmailTo" tag-placeholder="Add Email" placeholder="Search or add a tag" label="cc" track-by="id" :options="ccOptions" :multiple="true" :taggable="true" @tag="AddCCToList"></multiselect>

                                    </div>


                                    <div :key="render" class="form-group has-label col-sm-12 ">
                                        <label class="text  font-weight-bolder"> {{ $t('EmailCompose.Subject') }}:</label>
                                        <input class="form-control" v-model="emailCompoese.subject" type="text" />

                                    </div>
                                    <div class="form-group has-label col-sm-12 ">
                                        <label class="text  font-weight-bolder"> {{ $t('EmailCompose.Description') }}: </label>
                                        <textarea class="form-control"  v-model="emailCompoese.description" type="text" />

                                    </div>



                                </div>
                            </div>
                        </div>
                        <div v-if="!loading">
                            <div class="modal-footer justify-content-right">
                                <button type="button" class="btn btn-primary  " v-on:click="SendEmail"> {{ $t('EmailCompose.Save') }}</button>
                               
                                <button type="button" class="btn btn-danger  mr-3 " v-on:click="close()">{{ $t('EmailCompose.Close') }}</button>
                            </div>

                        </div>
                        <div v-else>
                            <loading v-model:active="loading" :can-cancel="false" :is-full-page="true"></loading>
                        </div>
                    </div>
                </div>
            </div>
            
        </div>
    </modal>
   
</template>
<script>
import clickMixin from '@/Mixins/clickMixin'
    
    import Multiselect from 'vue-multiselect'
      import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    
    export default {
        mixins: [clickMixin],
        props: ['show', 'documentId','headerFooter','email', 'formName'],
        components: {
            Loading,
            Multiselect
        },
        data: function () {
            return {
                arabic: '',
                english: '',
                render: 0,
                loading: false,
                ccOptions: [],
                printDetails:[],
                printRenderEmail:0,
                emailCompoese: {
                    EmailTo: [],
                    subject: '',
                    description: '',
                    companyName: '',
                    buttonName: '',
                    emailPath: '',
                }
            }
        },
        validations() {

        },
        methods: {
            

            AddCCToList: function (newEmail) {
                var uid = this.createUUID()
                const email = {
                    cc: newEmail,
                    id: uid
                }
                this.emailCompoese.EmailTo.push(email)
                this.ccOptions.push(email)
            },

            createUUID: function () {

                var dt = new Date().getTime();
                var uuid = 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
                    var r = (dt + Math.random() * 16) % 16 | 0;
                    dt = Math.floor(dt / 16);
                    return (c == 'x' ? r : (r & 0x3 | 0x8)).toString(16);
                });
                return uuid;
            },
            close: function () {
                this.$emit('close');
            },


            SendEmail: function () {

               
                this.$emit('update:modelValue', this.emailCompoese)
            },
        },
        mounted: function () {
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            this.cc = this.email

        }
    }</script>
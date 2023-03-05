using System.Collections.Generic;

namespace FileData
{
    namespace OpenAI
    {
        public class OpenAITrainFileData
        {
            public string id;
            public long bytes;
            public string filename;

            public override string ToString()
            {
                return $"id = {id}, bytes = {bytes}, filename = {filename}";
            }
        }

        public class TrainFileList
        {
            public List<OpenAITrainFileData> data;

            public override string ToString()
            {
                string result = "";
                foreach (var item in data)
                {
                    result += item.ToString() + "\n";
                }
                return result;
            }
        }

        public class FinetuneModelData
        {
            public string id;
            public string model;
            public long created_at;
            public string fine_tuned_model;
            public string organization_id;
            public string status;
            public long updated_at;

            public override string ToString()
            {
                return $"id = {id}, model = {model}, created_at = {created_at}, fine_tuned_model = {fine_tuned_model}, organization_id = {organization_id}, status = {status}, updated_at = {updated_at}";
            }
        }

        public class FinetuneModelList
        {
            public List<FinetuneModelData> data;

            public override string ToString()
            {
                string result = "";
                foreach (var item in data)
                {
                    result += item.ToString() + "\n";
                }
                return result;
            }
        }

        public class HyperParams
        {
            public int batch_size;
            public double learning_rate_multiplier;
            public int n_epochs;
            public double prompt_loss_weight;
        }
    }
}
